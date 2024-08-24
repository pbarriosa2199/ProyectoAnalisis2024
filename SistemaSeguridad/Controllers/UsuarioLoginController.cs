using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaSeguridad.Models;
using SistemaSeguridad.Servicios;

namespace SistemaSeguridad.Controllers
{
	public class UsuarioLoginController: Controller
	{
		private readonly UserManager<UsuarioPrueba> userManager;
		private readonly IServicioUsuarios servicioUsuarios;
		private readonly SignInManager<UsuarioPrueba> signInManager;

		public UsuarioLoginController(UserManager<UsuarioPrueba> userManager, IServicioUsuarios servicioUsuarios,
			SignInManager<UsuarioPrueba> signInManager)
		{
			this.userManager = userManager;
			this.servicioUsuarios = servicioUsuarios;
			this.signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel modelo)
		{
			if (!ModelState.IsValid)
			{
				return View(modelo);
			}

			var resultado = await signInManager.PasswordSignInAsync(modelo.IdUsuario, password: modelo.Password,
																	modelo.Recuerdame, lockoutOnFailure: false);
			if (resultado.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError(String.Empty, "Nombre de usuario o password incorrecto");
				return View(modelo);
			}
		}
	}
}
