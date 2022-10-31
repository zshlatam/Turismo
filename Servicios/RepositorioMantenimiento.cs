using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioMantenimiento
    {
        Task<IEnumerable<Mantenimiento>> Buscar();
        Task Crear(Mantenimiento mantenimiento);
    }
    public class RepositorioMantenimiento: IRepositorioMantenimiento
    {
        private readonly string connectionString;

        public RepositorioMantenimiento(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Mantenimiento mantenimiento)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("SP_INSERTAR_MANTENIMIENTO",
                new
                {
                    mantenimiento.Descripcion,
                    mantenimiento.Fecha,
                    mantenimiento.DepartamentoId
                }, commandType: System.Data.CommandType.StoredProcedure);
            mantenimiento.Id = id;
        }
        public async Task<IEnumerable<Mantenimiento>> Buscar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Mantenimiento>(@"
                                    Select mantenimiento.Id,Departamento.Descripcion as Departamento, 
                                    Mantenimiento.Descripcion, Mantenimiento.Fecha
                                    from Mantenimiento 
                                    join departamento
                                    on DepartamentoId = Departamento.Id;;");
        }
    }
}
