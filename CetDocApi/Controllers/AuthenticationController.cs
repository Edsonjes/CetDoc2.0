using Microsoft.AspNetCore.Mvc;
using Dominio.ViewModel;
using Dominio.Interfaces;

namespace CetDocsApi.Controllers
{
    public class AuthenticationController : Controller
    {

        public readonly IAuthentication _authentication;

        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

      public IActionResult Login()
        {
            return View();
        }

       [HttpPost]
       public async Task<IActionResult> Login(UserViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var token = await _authentication.Login(obj);
                if (token != null)
                {
                    HttpContext.Session.SetString("JWToken", token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                }

            }
            return View(obj);
        }
    }
}
