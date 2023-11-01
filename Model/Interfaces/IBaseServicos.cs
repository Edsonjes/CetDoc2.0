using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dominio.Interfaces
{
	public interface IBaseServicos<TEntidade> where TEntidade : class
	{
	    public Task<TEntidade> Cadastrar<TValidador>(TEntidade obj) where TValidador : AbstractValidator<TEntidade>;
	    public Task<TEntidade> Atualizar<TValidador>(TEntidade obj) where TValidador : AbstractValidator<TEntidade>;
		public Task<TEntidade> Excluir(TEntidade obj);
		public Task<TEntidade> BuscarPorId(int id);
		public Task<List<TEntidade>> Listar();
	}
}
