using Microsoft.AspNetCore.Mvc;
using Dominio.Modelos;
using Infra.Repository;
using FluentValidation;
using System.ComponentModel.DataAnnotations;


namespace CetDocsApi.Controllers
{
	public class PessoaController : Controller
	{
		public readonly PessoaRepository _pessoaRepository;

		public PessoaController(PessoaRepository pessoaRepository)
		{
			_pessoaRepository = pessoaRepository;
		}
	    
		public async Task<IActionResult> ListarTodasAPessoas()
		{
		  var ListPessoas = _pessoaRepository.Listar();

		  return (IActionResult) await ListPessoas;
			
		}

		public class PessoaValidator : AbstractValidator<Pessoa>
		{
			public PessoaValidator()
			{
				RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome não pode ser vazio");
				RuleFor(x => x.Email).NotEmpty().WithMessage("Email não pode ser vazio");
				RuleFor(x => x.Cpf).NotEmpty().WithMessage("Cpf não pode ser vazio");
				RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("Data de nascimento não pode ser vazio");
				RuleFor(x => x.Etinia).NotEmpty().WithMessage("Etnia não pode ser vazio");
			}
		}

		[HttpPost]
		[Route("CadastrarPessoa")]
		public async Task<IActionResult> CadastrarPessoa(Pessoa pessoa)
		{

			if (ModelState.IsValid)
			{
				var Cadastrar = _pessoaRepository.Cadastrar<PessoaValidator>(pessoa);
				return (IActionResult) await Cadastrar;
			}
			else
			{
				return BadRequest(ModelState);
			}

		}
	}
}
