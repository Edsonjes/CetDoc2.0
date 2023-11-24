using Dominio.Interfaces;
using Dominio;
using FluentValidation;
using Servicos;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Dominio.ViewModel;
using Dominio.Model;

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
		public readonly Mapper _Mapper;

		public PessoaRepository(PessoaServices pessoaServices, Mapper mapper)
		{
			PessoaServices = pessoaServices;
			_Mapper = mapper;
		}

		public async Task<PessoaViewModel> Cadastrar(PessoaViewModel obj) 
		{
			try
			{

				if (obj != null)
				{
					var mapObj= _Mapper.Map<Pessoa>(obj);
					var PessoaCadastro = PessoaServices.Cadastrar(mapObj);
				}
				
				return obj;

			}
			catch (Exception ex)
			{
				throw ex.InnerException;
			}
		}
	
		public Task<PessoaViewModel> Atualizar(PessoaViewModel obj)
		{
			throw new NotImplementedException();
		}

		public Task<PessoaViewModel> Excluir(PessoaViewModel obj)
		{
			throw new NotImplementedException();
		}

		public Task<PessoaViewModel> BuscarPorId(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<PessoaViewModel>> Listar()
		{
			throw new NotImplementedException();
		}
	}
}
