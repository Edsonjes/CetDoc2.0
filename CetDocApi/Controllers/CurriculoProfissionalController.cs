﻿using Dominio.Interfaces;
using Dominio.ViewModel;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CetDocsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculoProfissionalController : Controller
    {
        private readonly HttpClient _httpClient;
        public const string basePath = "CurriloProficional";
        private readonly ICurriculoProssional _curriculoProfissional;
        public CurriculoProfissionalController(ICurriculoProssional curriculoProfissional)
        {
            _curriculoProfissional = curriculoProfissional;
        }
        [HttpGet]
        [Route("ListarQuestoes")]
        public async Task<JsonResult> ListarQuestoesCurriculoProfissional()
        {
            try
            {
                List<CurriculoProfissionalViewModel> lista;
                lista = await _curriculoProfissional.ListarQuestoes();
                return new JsonResult (lista);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
