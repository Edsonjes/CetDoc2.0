using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Dominio;
using Dominio.Model;

namespace Servicos
{
    internal class Authentication
    {
        public  IConfiguration _Configuration;

        public Authentication(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        
        public async Task<bool> Login(User obj)
        {
            try
            {
                string sql = "SELECT * FROM tb_Usuario WHERE Login = @Login AND Senha = @Password";

                using (var connection = new SqlConnection(_Configuration.GetConnectionString("dbConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, obj);
                    obj.Id = result;
                    if (obj.Id == 0)
                    {
                        throw new Exception("Erro ao cadastrar pessoa");
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar pessoa");
            }
        }
    }
}
