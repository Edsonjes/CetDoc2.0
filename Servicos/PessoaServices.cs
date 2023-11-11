using Dominio.Modelos;
using FluentValidation;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Servicos
{
	public class PessoaServices
	{
		public IConfiguration _Configuration;
		public PessoaServices(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}

		public async Task<bool> Cadastrar(Pessoa obj)
		{
			try
			{
				string sql = "INSERT INTO Pessoa (Nome, Email, Etinia,Cpf,DataDeNascimento ) VALUES (@Nome, @Email,@Etinia,@Cpf,@DataDeNacimento ); SELECT CAST(SCOPE_IDENTITY() as int)";

				using (var connection = new SqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
				{
					connection.Open();
					var result = await connection.ExecuteAsync(sql, obj);
					obj.Id = result;
				}
				var retorno = bool.Parse("true");
				return retorno;

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

				string sql = "SELECT * FROM Pessoa";

				using (var connection = new SqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
				{
					connection.Open();
					var result = await connection.QueryAsync<Pessoa>(sql);
					return result.ToList();
				}
			}
			catch (Exception ex)
			{ throw ex.InnerException; }
		}
	}
}
