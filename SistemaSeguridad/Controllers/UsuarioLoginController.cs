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

            var usuario = await userManager.FindByNameAsync(modelo.IdUsuario);
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                return View(modelo);
            }

            // Comprobar si la cuenta está bloqueada
            if (usuario.FechaDesbloqueo.HasValue && usuario.FechaDesbloqueo.Value > DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Su cuenta está bloqueada temporalmente. Inténtelo de nuevo en 5 minutos.");
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.IdUsuario, password: modelo.Password,
                                                                    modelo.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                // Reiniciar contadores y fecha de desbloqueo
                usuario.IntentosDeAcceso = 0;
                usuario.FechaDesbloqueo = null;
                await userManager.UpdateAsync(usuario);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Incrementar el contador de intentos fallidos
                usuario.IntentosDeAcceso++;
                if (usuario.IntentosDeAcceso >= 5)
                {
                    usuario.FechaDesbloqueo = DateTime.Now;
                    await userManager.UpdateAsync(usuario);
                    ModelState.AddModelError(string.Empty, "Su cuenta ha sido bloqueada temporalmente. Inténtelo de nuevo en 5 minutos.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                }
                await userManager.UpdateAsync(usuario);
                return View(modelo);
            }
        }


    }
}

