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
		[Route("Login")]
        public async Task<IActionResult> Login(UserViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var token = await _authentication.Login(obj);

                if (token != null)
                {
                    
                    return new JsonResult(new { Token = token });
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha incorretos");
                }
            }

            return BadRequest(ModelState);
        }
    }
}
