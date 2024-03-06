using Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
	public class CurriculoProfissionalModel
	{
        public QuestoesModel Questoes { get; set; }
        public List<QuestoesModel> ListQuestoes { get; set; } = new();
        public Pessoa Pessoa { get; set; }
        public string Pontuacao { get; set; }
       
	}
}
