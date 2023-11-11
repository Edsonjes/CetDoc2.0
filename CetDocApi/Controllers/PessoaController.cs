using Microsoft.AspNetCore.Mvc;
using Dominio;
using FluentValidation;
using Dominio.Interfaces;
using Dominio.Modelos;
using Infra.Repository;

namespace CetDocsApi.Controllers
{
	public class PessoaController : Controller
	{
		public readonly IPessoaRepository _pessoaRepository;

		public PessoaController(IPessoaRepository pessoaRepository)
		{
			_pessoaRepository = pessoaRepository;
		}



		[HttpPost]
		[Route("CadastrarPessoa")]
		public async Task<IActionResult> CadastrarPessoa(Pessoa pessoa)
		{
			var cadPessoa = await _pessoaRepository.Cadastrar(pessoa);
			return Ok();
		}

	}


	
}

