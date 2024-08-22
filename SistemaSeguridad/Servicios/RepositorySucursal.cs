using Dapper;
using Microsoft.Data.SqlClient;
using SistemaSeguridad.Models;

namespace SistemaSeguridad.Servicios
{
	public interface IReposirorySucursal
	{
		Task Crear(Sucursal sucursal);
		Task<bool> Existe(string nombre);
		Task<IEnumerable<Sucursal>> Obtener();
	}
	public class RepositorySucursal: IReposirorySucursal
    {
        private readonly string connectionString;
        public RepositorySucursal(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Sucursal sucursal)
        {   
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("spsucursalinserta",
                                                             new
                                                             {
                                                                 Nombre = sucursal.Nombre,
                                                                 Direccion = sucursal.Direccion,
                                                                 IdEmpresa = sucursal.IdEmpresa,
                                                                 UsuarioCreacion = sucursal.UsuarioCreacion
                                                             },
                                                             commandType: System.Data.CommandType.StoredProcedure);
            sucursal.IdSucursal = id;
        }

        public async Task<bool> Existe(string nombre)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"select 1 from SUCURSAL where Nombre = @Nombre;",
                                                                        new { nombre });
            return existe == 1;
        }

		public async Task<IEnumerable<Sucursal>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Sucursal>(@"select IdSucursal, Nombre, Direccion from SUCURSAL");
        }
    }
}
