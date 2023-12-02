using Microsoft.AspNetCore.Mvc;
using Dominio;
using FluentValidation;
using Dominio.Interfaces;
using Dominio.Model;
using Infra.Repository;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace CetDocsApi.Controllers
{
	public class PessoaController : Controller
	{
		public readonly IPessoaRepository _pessoaRepository;

		public PessoaController(IPessoaRepository pessoaRepository)
		{
			_pessoaRepository = pessoaRepository;
		}


		[Authorize]
		[HttpPost]
		[Route("CadastrarPessoa")]
		public async Task<IActionResult> CadastrarPessoa(PessoaViewModel pessoa)
		{
			var cadPessoa = await _pessoaRepository.Cadastrar(pessoa);
			return Ok();
		}

	}


	
}

