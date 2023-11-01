using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		public T Cadastrar(T obj);
		public T Atualizar(T obj);
		public T Excluir(T obj);
		public T BuscarPorId(int id);
		public List<T> Listar();

	}
}
