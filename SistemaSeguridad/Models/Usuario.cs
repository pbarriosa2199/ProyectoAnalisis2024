namespace SistemaSeguridad.Models
{
	public class Usuario
	{
		public string IdUsuario { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public int IdStatusUsuario { get; set; }
		public string Password { get; set; }
		public int IdGenero { get; set; }
		public DateTime UltimaFechaIngreso { get; set; }
		public int IntentosDeAcceso { get; set; }
		public string SesionActual { get; set; }
		public DateTime UltimaFechaCambioPassword { get; set; }
		public string CorreoElectronico { get; set; }
		public int RequiereCambiarPassword { get; set; }
		public string Fotografia { get; set; }
		public string TelefonoMovil { get; set; }
		public int IdSucursal { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string UsuarioCreacion { get; set; }
		public DateTime FechaModificacion { get; set; }
		public string UsuarioModificacion { get; set; }

	}
}
