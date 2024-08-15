namespace SistemaSeguridad.Models
{
	public class Sucursal
	{
		public int IdSucursal { get; set; }
		public string Nombre { get; set; }
		public string Direccion { get; set; }
		public int IdEmpresa { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string UsuarioCreacion { get; set; }
		public DateTime FechaModificacion { get; set; }
		public string UsuarioModificacion { get; set; }

	}
}
