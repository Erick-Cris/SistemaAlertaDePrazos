using AlertaDePrazosLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AppAlertaDePrazos.Controllers
{
    public class CursoController : Controller
    {
        [Route("Curso/Get")]
        public IActionResult Get()
        {
            try
            {
                var client = new RestClient("https://localhost:7149");
                var request = new RestRequest("Curso/Get", Method.Get);
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
