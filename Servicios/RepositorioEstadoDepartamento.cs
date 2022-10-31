using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioEstadoDepartamento
    {
        Task Crear(EstadoDepartamento estadoDepartamento);
        Task<bool> Existe(string descripcion);
        Task<IEnumerable<EstadoDepartamento>> Obtener();
        Task<EstadoDepartamento> ObtenerPorId(int id);
    }
    public class RepositorioEstadoDepartamento: IRepositorioEstadoDepartamento
    {
        private readonly string connectionString;

        public RepositorioEstadoDepartamento(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(EstadoDepartamento estadoDepartamento)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO EstadoDepartamento (Descripcion)
                                                    Values (@Descripcion);
                                                    SELECT SCOPE_IDENTITY();", estadoDepartamento);
            estadoDepartamento.Id = id;
                                        
        }

        public async Task<bool> Existe(string descripcion)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 
                FROM EstadoDepartamento
                WHERE Descripcion = @descripcion;", new { descripcion });

            return existe == 1;
        }

        public async Task<IEnumerable<EstadoDepartamento>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<EstadoDepartamento>(@"Select Id, Descripcion 
                                                        FROM EstadoDepartamento;");
        }

        public async Task<EstadoDepartamento>ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<EstadoDepartamento>(@"Select Id, Descripcion 
                                                                        FROM EstadoDepartamento
                                                                        WHERE Id = @Id",
                                                                        new { id });
        }
    }
}
