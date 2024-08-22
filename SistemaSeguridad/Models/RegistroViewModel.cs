﻿using System.ComponentModel.DataAnnotations;

namespace SistemaSeguridad.Models
{
	public class RegistroViewModel
	{
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public string IdUsuario { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public string Nombre { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public string Apellido { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Fecha Nacimiento")]
		[DataType(DataType.Date)]
		public DateTime FechaNacimiento { get; set; } = DateTime.Today;
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[EmailAddress(ErrorMessage = "El campo debe ser un correo valido")]
		public string CorreoElectronico { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string TelefonoMovil { get; set; }
		public string UsuarioCreacion { get; set; }
	}
}
