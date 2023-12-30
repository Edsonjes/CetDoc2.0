using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
	public class EnderecoPessoa
	{
	    public int IdEndereco { get; set; }
		public int IdPessoa { get; set; }
		public string Cidade { get; set; }
		public string Estado { get; set; }
		public string Cep { get; set; }
		public string logradouro { get; set; }
	}
}
