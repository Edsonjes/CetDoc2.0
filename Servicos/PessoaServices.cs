using Dominio.Modelos;
using FluentValidation;
using Infra.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Servicos
{
	public class PessoaServices : PessoaRepository
	{
		public IConfiguration _Configuration;
		public PessoaServices(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}

		public async Task<Pessoa> Cadastrar<TValidador>(Pessoa obj) where TValidador : AbstractValidator<Pessoa>
		{
			try { }
			catch (Exception ex)
			{ 
				throw ex; 
			}
		} 

		public Task<Pessoa> Atualizar<TValidador>(Pessoa obj) where TValidador : AbstractValidator<Pessoa>
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

		public Task<List<Pessoa>> Listar()
		{
			throw new NotImplementedException();
		}
	}
}
