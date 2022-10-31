using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioCiudad
    {
        Task Crear(Ciudad ciudad);
        Task<bool> Existe(string descripcion);
        Task<IEnumerable<Ciudad>> Obtener();
        Task<Ciudad> ObtenerPorId(int id);
    }

    public class RepositorioCiudad: IRepositorioCiudad
    {
        private readonly string connectionString;
        public RepositorioCiudad(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        async Task IRepositorioCiudad.Crear(Ciudad ciudad)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Ciudad (Descripcion)
                                                    Values(@Descripcion);
                                                    SELECT SCOPE_IDENTITY();", ciudad);
            ciudad.Id = id;
        }

        async Task<bool> IRepositorioCiudad.Existe(string descripcion)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 
                FROM Ciudad
                WHERE Descripcion = @descripcion;",new {descripcion});

            return existe == 1;
        }

        async Task<IEnumerable<Ciudad>> IRepositorioCiudad.Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Ciudad>(@"Select Id, Descripcion 
                                                        FROM Ciudad;");
        }

        async Task<Ciudad> IRepositorioCiudad.ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Ciudad>(@"Select Id, Descripcion 
                                                                        FROM Ciudad
                                                                        WHERE Id = @Id",
                                                                        new { id });
        }

    }
}
