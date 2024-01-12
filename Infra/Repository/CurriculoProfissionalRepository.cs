using AutoMapper;
using Dominio.Interfaces;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Servicos;


namespace Infra.Repository
{
	public  class CurriculoProfissionalRepository : ICurriculoProssional
	{
	    private  IMapper _map;
		private readonly CurriculoProfissionalService _service;
        public IConfiguration _Configuration;

        public CurriculoProfissionalRepository(IMapper map, CurriculoProfissionalService service, IConfiguration configuration)
        {
            _map = map;
			_service = service;
			_Configuration = configuration;
        }
        
        public async Task<List<CurriculoProfissionalViewModel>> ListarQuestoes()
		{
			try
			{
				List<CurriculoProfissionalViewModel> Retorno;
                Retorno = _map.Map<List<CurriculoProfissionalViewModel>>(await _service.ListarQuestoes());
                return Retorno;
			}
			catch(Exception )
			{
				throw new Exception("Erro ao listar CurriculoProfissional");
			}
			
		}
	}
}
