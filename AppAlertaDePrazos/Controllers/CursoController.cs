using AlertaDePrazosLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AppAlertaDePrazos.Controllers
{
    public class CursoController : Controller
    {
        private IConfiguration _configuration;
        public CursoController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [Route("Curso/Get")]
        public IActionResult Get()
        {
            try
            {
                string apiToken = HttpContext.Session.GetString("SecurityTokenAPI");
                if (apiToken == null)
                    throw new Exception("Se autentique novamente para acessar este recurso.");

                string urlApiUfu = _configuration["Configuracoes:UrlApiUFU"];
                var client = new RestClient(urlApiUfu);
                var request = new RestRequest("Curso/Get", Method.Get);
                request.AddHeader("Authorization", "Bearer " + apiToken);
                var cursos = client.Execute<List<Curso>>(request).Data;

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
