using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaSeguridad.Models;
using SistemaSeguridad.Servicios;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaSeguridad.Controllers
{
	public class UsuarioController: Controller
	{
		private readonly UserManager<UsuarioPrueba> userManager;
		private readonly IServicioUsuarios servicioUsuarios;
		private readonly SignInManager<UsuarioPrueba> signInManager;
		private readonly RepositoryUsuarios repositoryUsuarios;

		public UsuarioController(UserManager<UsuarioPrueba> userManager, IServicioUsuarios servicioUsuarios,
			SignInManager<UsuarioPrueba> signInManager)
        {
			this.userManager = userManager;
			this.servicioUsuarios = servicioUsuarios;
			this.signInManager = signInManager;
		}

        public IActionResult Index()
        {
            return View();
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
				//await signInManager.SignInAsync(usuario, isPersistent: false);
				return RedirectToAction("Index", "Home");
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


		[HttpPost]
		public async Task<IActionResult> Logout() 
		{
			await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
			return RedirectToAction("Index", "Home");
		}

        public async Task<IActionResult> Perfil()
        {
			var usuarioLogin = await userManager.GetUserAsync(User);

            if (usuarioLogin == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var userId = usuarioLogin.IdUsuario;
            var userName = usuarioLogin.Nombre;
            var email = usuarioLogin.CorreoElectronico;
			var password = usuarioLogin.Password;

            var model = new UsuarioPrueba
            {
                IdUsuario = userId,
                Nombre = userName,
                CorreoElectronico = email,
				Password = password
            };

            return View(model);
        }
    }
}
