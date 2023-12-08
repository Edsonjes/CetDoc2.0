using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Dominio;
using Dominio.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Servicos
{
    public class Authentication
    {
        public IConfiguration _Configuration;

        public Authentication(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public async Task<string> Login(User obj)
        {
            try
            {
                string sql = "SELECT IdUsuario FROM tb_Usuario WHERE Login = @Login AND Senha = @Password";
                var key = Encoding.ASCII.GetBytes(_Configuration.GetSection("CriptoRash:Key").Value);
                var tokenHandler = new JwtSecurityTokenHandler();
                using (var connection = new SqlConnection(_Configuration.GetConnectionString("dbConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<int>(sql, new {obj.Login,obj.Password});
                   
                    if (result.Any())
                    {
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                        new Claim(ClaimTypes.Name, obj.Login)
                    }),
                            Expires = DateTime.UtcNow.AddHours(2),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenGenerated = tokenHandler.WriteToken(securityToken);

                        return tokenGenerated;
                    }
                    else
                    {
                        throw new Exception("Não foi possível gerar o token");

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
