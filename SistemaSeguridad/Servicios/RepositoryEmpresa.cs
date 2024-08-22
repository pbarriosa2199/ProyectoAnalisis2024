using Dapper;
using Microsoft.Data.SqlClient;
using SistemaSeguridad.Models;

namespace SistemaSeguridad.Servicios
{
	public interface IRepositoyEmpresa
	{
		Task Crear(Empresa empresa);
		Task<bool> Existe(string nombre);
		Task<IEnumerable<Empresa>> Obtener();
	}
	public class RepositoryEmpresa: IRepositoyEmpresa
	{
		private readonly string connectionString;

        public RepositoryEmpresa(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public async Task Crear(Empresa empresa)
		{
			using var connection = new SqlConnection(connectionString);
			var id = await connection.QuerySingleAsync<int>("spempresainserta",
															 new
															 { 
																Nombre = empresa.Nombre, Direccion = empresa.Direccion,
																PasswordCantidadMayusculas = empresa.PasswordCantidadMayusculas,
																PasswordCantidadMinusculas = empresa.PasswordCantidadMinusculas,
																PasswordCantidadCaracteresEspeciales = empresa.PasswordCantidadCaracteresEspeciales,
																PasswordCantidadCaducidadDias = empresa.PasswordCantidadCaducidadDias,
																PasswordLargo = empresa.PasswordLargo,
																PasswordIntentosAntesDeBloquear = empresa.PasswordIntentosAntesDeBloquear,
																PasswordCantidadNumeros = empresa.PasswordCantidadNumeros,
																PasswordCantidadPreguntasValidar = empresa.PasswordCantidadPreguntasValidar,
																UsuarioCreacion = empresa.UsuarioCreacion
															 },
															 commandType: System.Data.CommandType.StoredProcedure);
			empresa.IdEmpresa = id;
		}

		public async Task<bool> Existe(string nombre)
		{
			using var connection = new SqlConnection(connectionString);
			var existe = await connection.QueryFirstOrDefaultAsync<int>(@"select 1 from EMPRESA where Nombre = @Nombre;",
																		new { nombre });
			return existe == 1;
		}

		public async Task<IEnumerable<Empresa>> Obtener()
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryAsync<Empresa>(@"select IdEmpresa, Nombre, Direccion, Nit from EMPRESA");
		}

	}
}
