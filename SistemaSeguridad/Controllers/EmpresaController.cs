using Microsoft.AspNetCore.Mvc;
using SistemaSeguridad.Servicios;

namespace SistemaSeguridad.Controllers
{
	public class EmpresaController: Controller
	{
		private readonly IRepositoyEmpresa repositoyEmpresa;
		private readonly IServicioUsuarios servicioUsuarios;

		public EmpresaController(IRepositoyEmpresa repositoyEmpresa, IServicioUsuarios servicioUsuarios)
        {
			this.repositoyEmpresa = repositoyEmpresa;
			this.servicioUsuarios = servicioUsuarios;
		}
		public async Task<IActionResult> Index()
		{
			var empresa = await repositoyEmpresa.Obtener();
			return View(empresa);
		}
		public IActionResult Crear()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> VerifarEmpresa(string nombre)
		{
			var existeEmpresa = await repositoyEmpresa.Existe(nombre);
			if (existeEmpresa)
			{
				return Json($"El nombre {nombre} ya existe");
			}

			return Json(true);
		}
	}
}
