using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class Pessoa
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
    }
}
