using Dominio.Interfaces;
using Dominio.Modelos;
using FluentValidation;
using Servicos;

using Microsoft.Extensions.Configuration;

namespace Infra.Repository
{
	public class PessoaRepository : IPessoaRepository
	{
		public IConfiguration _Configuration;
		public PessoaRepository(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}
		public readonly PessoaServices PessoaServices;

		public PessoaRepository(PessoaServices pessoaServices)
		{
			PessoaServices = pessoaServices;
		}

		public async Task<bool> Cadastrar<TValidador>(Pessoa obj) where TValidador : AbstractValidator<bool>
		{
			try
			{
				bool retorno;
				if (obj != null)
				{
					var PessoaCadastro = PessoaServices.Cadastrar(obj);
					retorno = await PessoaCadastro;
					return retorno;
				}
				
				return false;
				
			}
			catch (Exception ex)
			{
				throw ex.InnerException;
			}
		}
		public Task<Pessoa> Atualizar<TValidador>(Pessoa obj) where TValidador : AbstractValidator<Pessoa>
		{
			return null;
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
		   List<Pessoa> pessoas = new List<Pessoa>();

			var Listar = PessoaServices.Listar();

		    return await Listar;


		}
	}
}
