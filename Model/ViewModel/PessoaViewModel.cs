using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ViewModel
{
	public class PessoaViewModel
	{
		public int IdPessoa { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Telefone { get; set; }
		public string Cpf { get; set; }
		public string Etinia { get; set; }
		public string Sexo { get; set; }
		public string DataNascimento { get; set; }
	}
}
