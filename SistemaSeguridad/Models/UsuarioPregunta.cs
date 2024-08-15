﻿namespace SistemaSeguridad.Models
{
	public class UsuarioPregunta
	{
        public int IdPregunta { get; set; }
        public string IdUsuario { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int OrdenPregunta { get; set; }
        public DateTime FechaCreacion { get; set; }
		public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
