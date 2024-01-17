using Dominio.Model;

using Microsoft.Data.SqlClient;

using Dapper;

using Microsoft.Extensions.Configuration;
using System.Data;


namespace Servicos
{
    public class PessoaServices
    {
        public IConfiguration _Configuration;
        private readonly IDbConnection _connection;
        public PessoaServices(IConfiguration Configuration, IDbConnection connection)
        {
            _Configuration = Configuration;
            _connection = connection;
        }

        public async Task Cadastrar(Pessoa obj)
        {
            try
            {
                string sql = "INSERT INTO Pessoa (Nome,Email,Etinia,Cpf,DataDeNascimento ) VALUES (@Nome, @Email, @Etinia, @Cpf, @DataDeNacimento ); SELECT CAST(SCOPE_IDENTITY() as int)";

                var result = await _connection.ExecuteAsync(sql, obj);
                obj.IdPessoa = result;
                if (obj.IdPessoa == 0)
                {
                    throw new Exception("Erro ao cadastrar pessoa");
                }

            }
            catch (Exception)
            {
                throw new Exception("Erro ao cadastrar pessoa");
            }
        }

        public Task<Pessoa> Atualizar(Pessoa obj)
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa> Excluir(Pessoa obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pessoa>> Listar() =>
             (await _connection.QueryAsync<Pessoa>("SELECT * FROM tb_Pessoa")).ToList();  
    }
}
