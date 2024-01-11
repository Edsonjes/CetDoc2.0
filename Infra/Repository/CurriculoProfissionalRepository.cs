using AutoMapper;
using Dominio.Interfaces;
using Dominio.ViewModel;
using Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
	internal class CurriculoProfissionalRepository : ICurriculoProssional
	{
	    private  IMapper _map;
		private readonly CurriculoProfissionalService _service;
		public CurriculoProfissionalRepository(IMapper map, CurriculoProfissionalService service)
        {
            _map = map;
			_service = service;
        }
        public List<CurriculoProfissionalViewModel> listarQuestoes()
		{
			try
			{
				List<CurriculoProfissionalViewModel> Retorno;
				Retorno = _map.Map<List<CurriculoProfissionalViewModel>>(_service.ListarQuestoes);
				return Retorno;
			}catch(Exception ex)
			{
				throw new Exception("Erro ao listar CurriculoProfissional");
			}
			
		}
	}
}
