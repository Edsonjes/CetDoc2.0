using CetDocsApp.Utils;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Mvc;



namespace CetefeSite.Controllers
{
    public class HomeController : Controller
    {
        public readonly IConfiguration _Configuration;
        private readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:4440";

        public HomeController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _Configuration = configuration;
        }

        public IActionResult Index()
        {
            ListaQuestoes().Wait();
            return View();
        }
        [HttpGet]
        [Route("Home/ListaQuestoes")]
        public async Task<IActionResult> ListaQuestoes()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl + "/api/CurriculoProfissional/ListarQuestoes");
                Console.WriteLine(response);

                 

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.ReadContentAsync<List<CurriculoProfissionalViewModel>>();
                    ViewBag.ListaQuestoes = jsonResult;
                    return new JsonResult(jsonResult);
                }
                else
                {

                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
