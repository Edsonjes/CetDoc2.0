﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
	public class CurriculoProfissionalModel
	{
		public int IdQuestao { get; set; }
		public int IdPessoa { get; set; }
		public string Nome { get; set; }
		public string Pontuacao { get; set; }
		public int IdSubTipos { get; set; }
	}
}
