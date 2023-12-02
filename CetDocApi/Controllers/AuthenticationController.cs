﻿using Microsoft.AspNetCore.Mvc;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Http;
using Infra.Repository;

namespace CetDocsApi.Controllers
{
    public class AuthenticationController : Controller
    {

        public readonly AuthenticationRepository _authentication;

        public AuthenticationController(AuthenticationRepository authentication)
        {
            _authentication = authentication;
        }

      public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(UserViewModel obj)
		{
			if (ModelState.IsValid)
			{
				var token = await _authentication.Login(obj);
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
