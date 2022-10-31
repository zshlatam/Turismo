using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioEstadoReserva
    {
        Task Crear(EstadoReserva estadoReserva);
        Task<bool> Existe(string descripcion);
        Task<IEnumerable<EstadoReserva>> Obtener();
        Task<EstadoReserva> ObtenerPorId(int id);
    }

    public class RepositorioEstadoReserva: IRepositorioEstadoReserva
    {
        private readonly string connectionString;
        public RepositorioEstadoReserva(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(EstadoReserva estadoReserva)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO EstadoReserva (Descripcion)
                                                    Values(@Descripcion);
                                                    SELECT SCOPE_IDENTITY();", estadoReserva);
            estadoReserva.Id = id;
        }

        public async Task<bool> Existe(string descripcion)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 
                FROM EstadoReserva
                WHERE Descripcion = @descripcion;", new { descripcion });

            return existe == 1;
        }

        public async Task<IEnumerable<EstadoReserva>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<EstadoReserva>(@"Select Id, Descripcion 
                                                        FROM EstadoReserva;");
        }

        public async Task<EstadoReserva> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<EstadoReserva>(@"Select Id, Descripcion 
                                                                        FROM EstadoReserva
                                                                        WHERE Id = @Id",
                                                                        new { id });
        }



    }
}
