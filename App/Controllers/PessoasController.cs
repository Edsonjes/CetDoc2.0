using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	public class PessoasController : Controller
	{
		public IActionResult PessoasIndex()
		{
			return View();
		}
	}
}
