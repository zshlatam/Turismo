using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioReserva
    {
        Task<IEnumerable<Reserva>> Buscar();
        Task Cancelar(int id);
        Task Crear(Reserva reserva);
        //Task<IEnumerable<Reserva>> ObtenerIdReserva();
        Task<Reserva> ObtenerPorId(int id);
        //Task<IEnumerable<Reserva>> ObtenerDepartamentosDisponibles();
    }
    public class RepositorioReserva: IRepositorioReserva
    {
        private readonly string connectionString;

        public RepositorioReserva(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Reserva reserva)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("SP_INSERTAR_RESERVA",
                                        new
                                        {
                                            reserva.FechaReserva,
                                            reserva.FechaTerminoReserva,
                                            reserva.FechaPedidoReserva,
                                            reserva.DepartamentoId,
                                            reserva.EstadoReservaId,
                                            reserva.UsuarioId
                                        },
                                                commandType: System.Data.CommandType.StoredProcedure);
            reserva.Id = id;
        }


        //public async Task<IEnumerable<Reserva>> ObtenerDepartamentosDisponibles(DateTime FechaReserva,
        //                                                DateTime FechaTerminoReserva, int CiudadId)
        //{
        //    using var connection = new SqlConnection(connectionString);
        //    return await connection.QueryAsync<Reserva>(@"SELECT d.ciudadId,DepartamentoId FROM reserva join Departamento as d on DepartamentoId = d.Id
        //                                                WHERE FechaReserva NOT BETWEEN 
        //                                                @FechaReserva AND @FechaTerminoReserva 
        //                                                and d.CiudadId = @CiudadId", new { FechaReserva,
        //                                                    FechaTerminoReserva, CiudadId});
        //}
        public async Task<IEnumerable<Reserva>> Buscar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Reserva>(@"
                                     Select Reserva.Id,
                                        departamento.Descripcion as Departamento, 
                                        Reserva.FechaPedidoReserva,
                                        Reserva.FechaReserva,
                                        Reserva.FechaTerminoReserva,
                                        EstadoReserva.Descripcion as EstadoReserva
                                        from Reserva
                                        join Departamento
                                        on Reserva.DepartamentoId = Departamento.Id
                                        join	EstadoReserva
                                        on EstadoReserva.Id = Reserva.EstadoReservaId;");
        }

        public async Task<Reserva> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Reserva>(
                                    @"Select Reserva.Id,
                                        departamento.Descripcion as Departamento, 
                                        Reserva.FechaPedidoReserva,
                                        Reserva.FechaReserva,
                                        Reserva.FechaTerminoReserva,
                                        EstadoReserva.Descripcion as EstadoReserva
                                        from Reserva
                                        join Departamento
                                        on Reserva.DepartamentoId = Departamento.Id
                                        join	EstadoReserva
                                        on EstadoReserva.Id = Reserva.EstadoReservaId
										where Reserva.Id = @Id", new { id });
        }


        public async Task Cancelar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"
                                    UPDATE  Reserva 
                                    SET EstadoReservaId = 2
                                    WHERE Id = @Id;", new { id });
        }


        //public async Task<IEnumerable<Reserva>> ObtenerIdReserva()
        //{
        //    using var connection = new SqlConnection(connectionString);
        //    return await connection.QueryAsync<Reserva>(@"Select Id, Descripcion
        //                                                FROM Reserva;");
        //}
    }
}
