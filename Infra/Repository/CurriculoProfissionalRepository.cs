using AutoMapper;
using Dominio.Interfaces;
using Dominio.Model;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Servicos;


namespace Infra.Repository
{
    public class CurriculoProfissionalRepository : ICurriculoProssional
    {
        private IMapper _map;
        private readonly CurriculoProfissionalService _service;
        public IConfiguration _Configuration;

        public CurriculoProfissionalRepository(IMapper map, CurriculoProfissionalService service, IConfiguration configuration)
        {
            _map = map;
            _service = service;
            _Configuration = configuration;
        }

        public async Task<List<QuestoesViewModel>> ListarQuestoes()
        {
            try
            {
                List<QuestoesViewModel> Retorno;
                Retorno = _map.Map<List<QuestoesViewModel>>(await _service.ListarQuestoes());
                return Retorno;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao listar CurriculoProfissional");
            }

        }

        public async Task<CurriculoProfissionalViewModel> SalvarFormulario(CurriculoProfissionalViewModel obj)
        {
            try
            {
                CurriculoProfissionalViewModel Retorno = new CurriculoProfissionalViewModel();
                if (obj != null)
                {
                    var result = _map.Map<CurriculoProfissionalModel>(obj);
                    var CurriculoProfissionalCadastro =  _service.Cadastrar(result);
                   return Retorno = _map.Map<CurriculoProfissionalViewModel>(CurriculoProfissionalCadastro);
                }
                return Retorno;
       
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao salvar CurriculoProfissional");
            }
           
        }
    }
}
