﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	internal class User : Pessoa
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
	}
}
