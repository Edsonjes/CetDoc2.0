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
   
		public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}