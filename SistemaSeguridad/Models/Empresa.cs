using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SistemaSeguridad.Models
{
	public class Empresa
	{
        public int IdEmpresa { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Remote(action: "VerifarEmpresa", controller: "Empresa")]
		public string Nombre { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public string Direccion { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
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
