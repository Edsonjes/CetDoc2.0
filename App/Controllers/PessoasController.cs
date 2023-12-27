using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.ViewModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace App.Controllers
{
    public class PessoasController : Controller
	{
		public readonly IPessoaRepository _pessoaRepository;
		public readonly IConfiguration _Configuration;
		public PessoasController(IPessoaRepository pessoaRepository,IConfiguration configuration)
		{
            _pessoaRepository = pessoaRepository;
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

			List<PessoaViewModel> jsonResult = await _pessoaRepository.Listar();
			return new JsonResult(jsonResult);

		}
	}
}
