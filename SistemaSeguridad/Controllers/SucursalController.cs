using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using SistemaSeguridad.Models;
using SistemaSeguridad.Servicios;

namespace SistemaSeguridad.Controllers
{
    public class SucursalController: Controller
    {
        private readonly IReposirorySucursal reposirorySucursal;
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositoyEmpresa repositoyEmpresa;

        public SucursalController(IReposirorySucursal reposirorySucursal, IServicioUsuarios servicioUsuarios, 
            IRepositoyEmpresa repositoyEmpresa)
        {
            this.reposirorySucursal = reposirorySucursal;
            this.servicioUsuarios = servicioUsuarios;
            this.repositoyEmpresa = repositoyEmpresa;
        }

        public async Task<IActionResult> Index()
        {
            var sucursal = await reposirorySucursal.Obtener();
            return View(sucursal);
        }


        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var modelo = new SucursalCreacionViewModel();
            modelo.Empresa = await ObtenerEmpresas();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(SucursalCreacionViewModel sucursal) 
        {
			sucursal.UsuarioCreacion = servicioUsuarios.ObtenerUsuarioId();
            sucursal.Empresa = await ObtenerEmpresas();
            await reposirorySucursal.Crear(sucursal);
            return RedirectToAction("Index");
		}

        private async Task<IEnumerable<SelectListItem>> ObtenerEmpresas() 
        {
			var empresa = await repositoyEmpresa.Obtener();
			return empresa.Select(x => new SelectListItem(x.Nombre, x.IdEmpresa.ToString()));
		}

		[HttpGet]
		public async Task<IActionResult> VerifarSucursal(string nombre)
		{
			var existeGenero = await reposirorySucursal.Existe(nombre);
			if (existeGenero)
			{
				return Json($"El nombre {nombre} ya existe");
			}

			return Json(true);
		}
	}
}
