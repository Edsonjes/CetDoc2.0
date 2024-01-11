using Microsoft.AspNetCore.Mvc;

namespace CetDocsApi.Controllers
{
	[Route("api/[controller]")]
	public class CurriculoProfissionalController : Controller
	{
		[HttpGet]
		[Route("ListarQuestoesCurriculoProfissional")]
		public async Task<IActionResult> ListarQuestoesCurriculoProfissional()
		{
			return Ok();
		}
	}
}
