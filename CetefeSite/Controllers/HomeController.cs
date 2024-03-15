using CetDocsApp.Utils;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;




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
                    var jsonResult = await response.ReadContentAsync<List<QuestoesViewModel>>();
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
        [HttpPost]
        [Route("Home/SalvarFormulario")]
        public async Task<JsonResult> SalvarFormulario( CurriculoProfissionalViewModel cv)
        {
            try
            {
             
                var response = await _httpClient.PostAsJsonAsync(BaseUrl + "/api/CurriculoProfissional/SalvarFormulario", cv);
                Console.WriteLine(response);

                return new JsonResult("Formulário salvo com sucesso!");
            }
            catch (Exception ex)
            {
               return new JsonResult(ex.Message);
            }

        }
    }
}
