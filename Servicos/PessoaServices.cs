using Dominio.Model;

using Microsoft.Data.SqlClient;

using Dapper;

using Microsoft.Extensions.Configuration;


namespace Servicos
{
	public class PessoaServices
	{
		public IConfiguration _Configuration;
		public PessoaServices(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}

		public async Task  Cadastrar(Pessoa obj)
		{
			try
			{
				string sql = "INSERT INTO tbl_Pessoa (Nome, Email, Cpf,DataDeNacimento ) VALUES (@Nome, @Email,@Etinia,@Cpf,@DataDeNacimento ); SELECT CAST(SCOPE_IDENTITY() as int)";

				using (var connection = new SqlConnection(_Configuration.GetConnectionString("dbConnection")))
				{
					connection.Open();
					var result = await connection.ExecuteAsync(sql, obj);
					obj.IdPessoa = result;
					if (obj.IdPessoa == 0)
					{
						throw new Exception("Erro ao cadastrar pessoa");
					}
				}
			}
			catch (Exception ex)
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

        public async Task<List<Pessoa>> Listar()
        {
            try
            {
                IEnumerable<Pessoa> listaRetorno;
                string sql = "SELECT * FROM tbl_Pessoa";

                using (var connection = new SqlConnection(_Configuration.GetConnectionString("dbConnection")))
                {
                    connection.Open();
                    listaRetorno = await connection.QueryAsync<Pessoa>(sql);
                    return listaRetorno.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
