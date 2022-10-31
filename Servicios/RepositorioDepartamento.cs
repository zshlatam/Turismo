using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioDepartamento
    {
        Task Actualizar(DepartamentoCreacionViewModel departamento);
        Task Borrar(int id);
        Task<IEnumerable<Departamento>> Buscar();
        Task Crear(Departamento departamento);
        Task<IEnumerable<Departamento>> ObtenerIdDepartamento();
        Task<Departamento> ObtenerPorId(int id);
    }
    public class RepositorioDepartamento: IRepositorioDepartamento
    {
        private readonly string connectionString;

        public RepositorioDepartamento(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Departamento departamento)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                        @"INSERT INTO Departamento 
                        (NroDepartamento,NroPiso,NroHabitaciones,Descripcion,Direccion,
                        Bano,Cocina,Piscina,Patio,Estacionamiento,Jacuzzi,Tv,
                        Parrilla,Fumadores,Wifi,Lavadora,CamaraSeguridad,AireAcondicionado,EstadoDepartamentoId,CiudadId)
                        Values(@NroDepartamento,@NroPiso,@NroHabitaciones,@Descripcion,
                        @Direccion,@Bano,@Cocina,@Piscina,@Patio,@Estacionamiento,@Jacuzzi,
                        @Tv,@Parrilla,@Fumadores,
                        @Wifi,@Lavadora,@CamaraSeguridad,@AireAcondicionado,@EstadoDepartamentoId,@CiudadId)
                        SELECT SCOPE_IDENTITY();", departamento);

            departamento.Id = id;

        }

        public async Task<IEnumerable<Departamento>> Buscar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Departamento>(@"
                                    SELECT d.Id,d.descripcion, d.NroDepartamento, d.NroHabitaciones, d.Direccion, 
                                    c.Descripcion as Ciudad, e.descripcion as Estado
                                    FROM Departamento d
									JOIN Ciudad c
                                    ON c.Id = d.CiudadId
                                    JOIN EstadoDepartamento e
                                    ON e.Id = d.EstadoDepartamentoId;");
        }


        public async Task Actualizar(DepartamentoCreacionViewModel departamento)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Departamento
                                    SET NroDepartamento = @NroDepartamento,  
                                    NroPiso = @NroPiso,
                                    NroHabitaciones = @NroHabitaciones,
                                    Descripcion = @Descripcion,
                                    Direccion = @Direccion,
                                    Bano = @Bano,
                                    Cocina = @Cocina,
                                    Piscina = @Piscina,
                                    Patio = @Patio,
                                    Estacionamiento = @Estacionamiento,
                                    Jacuzzi = @Jacuzzi,
                                    Tv = @Tv,
                                    Parrilla = @Parrilla,
                                    Fumadores = @Fumadores,
                                    Wifi = @Wifi,
                                    Lavadora = @Lavadora,
                                    CamaraSeguridad = @CamaraSeguridad,
                                    AireAcondicionado = @AireAcondicionado,
                                    EstadoDepartamentoId = @EstadoDepartamentoId,
                                    CiudadId = @CiudadId
                                    WHERE Id = @Id;", departamento);

        }


        public async Task<Departamento>ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Departamento>(
                                    @"SELECT NroDepartamento,NroPiso,NroHabitaciones,Descripcion,Direccion,
                                    Bano,Cocina,Piscina,Patio,Estacionamiento,Jacuzzi,Tv,
                                    Parrilla,Fumadores,Wifi,Lavadora,CamaraSeguridad,AireAcondicionado,
                                    EstadoDepartamentoId,CiudadId
                                    FROM Departamento
                                    WHERE Id = @Id",new { id });
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Departamento WHERE Id = @Id", new { id });
        }


        public async Task<IEnumerable<Departamento>> ObtenerIdDepartamento()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Departamento>(@"Select Id, direccion
                                                        FROM Departamento;");
        }

        
    }

    
}
