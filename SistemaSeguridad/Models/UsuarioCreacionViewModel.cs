using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaSeguridad.Models
{
    public class UsuarioCreacionViewModel: Usuario
    {
        public IEnumerable<SelectListItem> Genero { get; set; }

		public IEnumerable<SelectListItem> Sucursal { get; set; }
	}
}
