using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ViewModel
{
	public class CurriculoProfissionalViewModel
	{

        public PessoaViewModel Pessoa { get; set; }

        public List<QuestoesViewModel> ListQuestoes { get; set; } = new();

        //public string Pontuacao { get; set; }

    }
}
