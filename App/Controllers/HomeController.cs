using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Dominio.ViewModel;
using Dominio.Interfaces;
using Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthentication _IAuthentication;

        public HomeController(ILogger<HomeController> logger, IAuthentication authentication)
        {
            _logger = logger;
            _IAuthentication = authentication;
        }


		[Authorize]
		[Route("Home/Index")]
		public IActionResult Index()
        {
            return View();
        }
		public IActionResult Login() => View();

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(UserViewModel obj)
		{
			if (ModelState.IsValid)
			{
				var token = await _IAuthentication.Login(obj);
				if (token != null)
				{
					HttpContext.Session.SetString("JWToken", token.ToString());
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Usuário ou senha incorretos");
				}
			}
			return View();
		}

	}
}