using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioServiciosDepartamento
    {
        void Crear(ServiciosDepartamento serviciosDepartamento);
    }
    public class RepositorioServiciosDepartamento:IRepositorioServiciosDepartamento
    {
        private readonly string connectionString;

        public RepositorioServiciosDepartamento(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Crear(ServiciosDepartamento serviciosDepartamento)
        {
            using var connection = new SqlConnection(connectionString);
            var id = connection.QuerySingle<int>(@"INSERT INTO ServiciosDepartamento 
                                                (Bano,Cocina,Piscina,PatioTrasero,Estacionamiento,Jacuzzi,Tv,
                                                    Parilla,Fumadores,Wifi,Lavadora,CamaraSeguridad,AireAcondicionado)
                                                    Values(@Bano,@Cocina,@Piscina,@PatioTrasero,@Estacionamiento,@Jacuzzi,@Tv,@Parilla,@Fumadores,
                                                    @Wifi,@Lavadora,@CamaraSeguridad,@AireAcondicionado)
                                                    SELECT SCOPE_IDENTITY();", serviciosDepartamento);

            serviciosDepartamento.Id = id;

        }
    }
}
