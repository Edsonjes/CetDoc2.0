using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ViewModel
{
    public class QuestoesViewModel
    {
        public int IdQuestao { get; set; }
        public string Nome { get; set; }
        public string Value { get; set; }
        public string Resposta { get; set; }
        public string Pontuacao { get; set; }
        public int IdSubTipos { get; set; }
    }
}
