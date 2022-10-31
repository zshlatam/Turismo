using Dapper;
using Microsoft.Data.SqlClient;
using Turismo.Models;

namespace Turismo.Servicios
{
    public interface IRepositorioUsuario
    {
        Task<Usuario> Buscar();
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }
    public class RepositorioUsuario:IRepositorioUsuario
    {
        private readonly string connectionString;

        public RepositorioUsuario(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                                        @"INSERT INTO Usuario (Email,EmailNormalizado,PasswordHash)
                                        Values(@Email,@EmailNormalizado,@PasswordHash);
                                        SELECT SCOPE_IDENTITY();", usuario);

            return id;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Usuario>(
                                    @"SELECT * FROM Usuario WHERE EmailNormalizado = @emailNormalizado",
                                    new { emailNormalizado });
        }
        public async Task<Usuario> Buscar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Usuario>(
                                    @"SELECT Email FROM Usuario");
        }


    }
}
