using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.ViewModel;

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
		public async Task<IActionResult> ListarPessoas()
		{
            List<PessoaViewModel> lista = new List<PessoaViewModel>();
			lista = await _pessoaRepository.Listar();
			return (IActionResult)lista;
        }
	}
}
