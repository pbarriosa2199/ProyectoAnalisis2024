using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaSeguridad.Models;
using SistemaSeguridad.Servicios;
using System.Security.Claims;

namespace SistemaSeguridad.Controllers
{
	public class UsuarioController: Controller
	{
		private readonly UserManager<UsuarioPrueba> userManager;
		private readonly IServicioUsuarios servicioUsuarios;
		private readonly SignInManager<UsuarioPrueba> signInManager;

		public UsuarioController(UserManager<UsuarioPrueba> userManager, IServicioUsuarios servicioUsuarios,
			SignInManager<UsuarioPrueba> signInManager)
        {
			this.userManager = userManager;
			this.servicioUsuarios = servicioUsuarios;
			this.signInManager = signInManager;
		}

        public IActionResult Registro() 
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Registro(RegistroViewModel modelo)
		{
			if (!ModelState.IsValid) 
			{
				return View(modelo);
			}


			var usuario = new UsuarioPrueba() { CorreoElectronico = modelo.CorreoElectronico, IdUsuario = modelo.IdUsuario,
												Nombre = modelo.Nombre, Apellido = modelo.Apellido, 
												FechaNacimiento = modelo.FechaNacimiento, TelefonoMovil = modelo.TelefonoMovil,
												UsuarioCreacion = servicioUsuarios.ObtenerUsuarioId()};

			var resultado = await userManager.CreateAsync(usuario, password:modelo.Password);

			if (resultado.Succeeded)
			{
				await signInManager.SignInAsync(usuario, isPersistent: true);
				return RedirectToAction("Index", "Genero");
			}
			else 
			{
				foreach (var error in resultado.Errors) 
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(modelo);
		}

		[HttpGet]
		public IActionResult Login() 
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel modelo) 
		{
			if (!ModelState.IsValid) 
			{
				return View(modelo);
			}

			var resultado = await signInManager.PasswordSignInAsync(modelo.IdUsuario, password:modelo.Password,
																	modelo.Recuerdame, lockoutOnFailure:false);
			if (resultado.Succeeded)
			{
				return RedirectToAction("Index", "Genero");
			}
			else 
			{
				ModelState.AddModelError(String.Empty, "Nombre de usuario o password incorrecto");
				return View(modelo);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Logout() 
		{
			await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
			return RedirectToAction("Index", "Genero");
		}
	}
}
