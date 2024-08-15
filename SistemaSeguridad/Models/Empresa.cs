namespace SistemaSeguridad.Models
{
	public class Empresa
	{
        public int IdEmpresa { get; set; }
		public string Nombre { get; set; }
        public string Direccion { get; set; }
		public string Nit { get; set; }
		public int PasswordCantidadMayusculas { get; set; }
		public int PasswordCantidadMinusculas { get; set; }
		public int PasswordCantidadCaracteresEspeciales { get; set; }
		public int PasswordCantidadCaducidadDias { get; set; }
		public int PasswordLargo { get; set; }
		public int PasswordIntentosAntesDeBloquear { get; set; }
		public int PasswordCantidadNumeros { get; set; }
		public int PasswordCantidadPreguntasValidar { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string UsuarioCreacion { get; set; }
		public DateTime FechaModificacion { get; set; }
		public string UsuarioModificacion { get; set; }

	}
}
