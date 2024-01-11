using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.ViewModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using CetDocsApp.Utils;

namespace App.Controllers
{
    public class PessoasController : Controller
	{
		
		public readonly IConfiguration _Configuration;
		private readonly HttpClient _httpClient;
		public const string BaseUrl = "https://localhost:5001/api/";
		public PessoasController(HttpClient httpClient,IConfiguration configuration)
		{
            _httpClient = httpClient;
			_Configuration = configuration;
        }
		public IActionResult PessoasIndex()
		{
			return View(ListarPessoas());
		}

		[Authorize]
		public IActionResult CadastrarPessoa()
		{
            return View();
        }

		[Authorize]
		[HttpGet]
		public async Task<JsonResult> ListarPessoas()
		{
			var response = await _httpClient.GetAsync(BaseUrl + "/ListarPessoas");
			var jsonResult = await response.ReadContentAsync<List<PessoaViewModel>>();
			return new JsonResult(jsonResult);
		}

		//[Authorize]
		//[HttpGet]
		//public async Task<JsonResult> ListarQuestoesCurriculoProfissional()
		//{
		//	var response = await _httpClient.GetAsync(BaseUrl + "/ListarQuestoesCurriculoProfissional");
		//}
	}
}
