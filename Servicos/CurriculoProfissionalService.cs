using Dapper;
using Dominio.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos
{
	
	public class CurriculoProfissionalService
	{
		public IConfiguration _Configuration;
		public CurriculoProfissionalService(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}

		public async Task Cadastrar(CurriculoProfissionalModel obj)
		{
			try
			{
				string sql = "INSERT INTO CurriculoProfissional (IdPessoa, IdCurriculo, IdProfissional) VALUES (@IdPessoa, @IdCurriculo, @IdProfissional); SELECT CAST(SCOPE_IDENTITY() as int)";

				using (var connection = new SqlConnection(_Configuration.GetConnectionString("dbConnection")))
				{
					connection.Open();
					var result = await connection.ExecuteAsync(sql, obj);
					obj.IdQuestao = result;
					if (obj.IdQuestao == 0)
					{
						throw new Exception("Erro ao cadastrar CurriculoProfissional");
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao cadastrar CurriculoProfissional");
			}
		}

		public async Task<CurriculoProfissionalModel> Atualizar(CurriculoProfissionalModel obj)
		{
			try
			{
				string sql = "UPDATE CurriculoProfissional SET IdPessoa = @IdPessoa, IdCurriculo = @IdCurriculo, IdProfissional = @IdProfissional WHERE IdQuestao = @IdQuestao";

				using (var connection = new SqlConnection(_Configuration.GetConnectionString("dbConnection")))
				{
					connection.Open();
					var result = await connection.ExecuteAsync(sql, obj);
					if (result == 0)
					{
						throw new Exception("Erro ao atualizar CurriculoProfissional");
					}
				}
				return obj;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao atualizar CurriculoProfissional");
			}
		}

		public async Task<CurriculoProfissionalModel> ListarQuestoes()
		{
			try
			{
				IEnumerable<CurriculoProfissionalModel> listaRetorno;
				string sql = "SELECT * FROM tbl_SubTipoQuestoes";
				using (var connection = new SqlConnection(_Configuration.GetConnectionString("dbConnection")))
				{
					connection.Open();
					listaRetorno = await connection.QueryAsync<CurriculoProfissionalModel>(sql);
				}
				return listaRetorno.FirstOrDefault();
			}
			catch(Exception ex)
			{
				throw ex.InnerException;
			}
		}
	}
}
