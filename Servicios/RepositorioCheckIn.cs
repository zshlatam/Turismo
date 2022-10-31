using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioCheckIn
    {
        Task Crear(CheckIn checkIn);
        Task<CheckIn> ObtenerPorId(int id);
    }
    public class RepositorioCheckIn: IRepositorioCheckIn
    {
        private readonly string connectionString;

        public RepositorioCheckIn(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }




        public async Task Crear(CheckIn checkIn)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO CheckIn 
                                            (Descripcion,Fecha, ReservaId)
                                                    Values(@Descripcion,@Fecha,@ReservaId);
                                                    SELECT SCOPE_IDENTITY();",checkIn);
            checkIn.Id = id;

        }

        public async Task<CheckIn> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<CheckIn>(
                                    @"SELECT c.Id,c.Descripcion,c.Fecha,c.ReservaId, 
                                    r.FechaReserva,r.FechaPedidoReserva, u.Email , d.Descripcion as departamento, d.Direccion
                                    FROM CheckIn as c join RESERVA AS r 
                                    ON ReservaId = r.Id
                                    join Usuario as u on u.Id = r.UsuarioId 
                                    join Departamento as d on d.Id = r.DepartamentoId
                                    WHERE c.Id = @Id", new { id });

        }





        //public async Task Cancelar(int id)
        //{
        //    using var connection = new SqlConnection(connectionString);
        //    await connection.ExecuteAsync(@"
        //                            UPDATE  Reserva 
        //                            SET EstadoReservaId = 2
        //                            WHERE Id = @Id;", new { id });
        //}
    }
}
