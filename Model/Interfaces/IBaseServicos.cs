
using FluentValidation;

namespace Dominio.Interfaces
{
	public interface IBaseServicos<TEntidade> where TEntidade : class
	{
		public Task<TEntidade> Cadastrar(TEntidade obj);
		public Task<TEntidade> Atualizar(TEntidade obj);
		public Task<TEntidade> Excluir(TEntidade obj);
		public Task<TEntidade> BuscarPorId(int id);
		public Task<List<TEntidade>> Listar();
	}
}
