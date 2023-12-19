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
        public readonly PessoaServices _PessoaServices;
        public Mapper _Mapper;
        public PessoaRepository(IConfiguration Configuration, PessoaServices PessoaServices)
		{
			_Configuration = Configuration;
            _PessoaServices = PessoaServices;

            _Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PessoaViewModel, Pessoa>().ReverseMap();
            }));
        }
		
		public async Task<PessoaViewModel> Cadastrar(PessoaViewModel obj)
		{
			try
			{

				if (obj != null)
				{
					var mapObj = _Mapper.Map<Pessoa>(obj);
					var PessoaCadastro =  _PessoaServices.Cadastrar(mapObj);
				}

				return obj;

			}
			catch (Exception ex)
			{
				throw ex.InnerException;
			}
		}

        public async Task<List<PessoaViewModel>> Listar()
        {
			try
			{
               
                var mapObj = await _PessoaServices.Listar();

                
                if (mapObj == null)
                {
                    throw new Exception("Lista de pessoas retornou nula");
                }

                var retorno = _Mapper.Map<List<PessoaViewModel>>(mapObj);

                if (retorno == null)
                {
                    throw new Exception("Erro ao mapear lista de pessoas para ViewModel");
                }

                return retorno;

            }
			catch(Exception ex)
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

		
	}
}
