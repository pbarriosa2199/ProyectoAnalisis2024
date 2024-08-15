using Microsoft.AspNetCore.Mvc;

namespace SistemaSeguridad.Controllers
{
	public class UsuarioPreguntaController: Controller
	{
		public IActionResult Crear() 
		{
			return View();
		}
	}
}
