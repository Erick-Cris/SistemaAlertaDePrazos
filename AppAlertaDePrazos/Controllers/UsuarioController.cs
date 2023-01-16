using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace AppAlertaDePrazos.Controllers
{
    public class UsuarioController : Controller
    {
        [Route("Usuario/Ativar/{token}")]
        public IActionResult Ativar(string token)
        {

            try
            {
                var url = $"https://localhost:7049/Usuario/Ativar/?token={token}";
                var client = new RestClient(url);
                var request = new RestRequest();
                var response = client.Get(request);
                if (response.IsSuccessStatusCode)
                    return View(true);
                else
                    return View(false);
            }
            catch (Exception e)
            {
                return View(false);
            }
        }

        [HttpGet]
        [Route("Usuario/Criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("Usuario/Criar")]
        public IActionResult Criar(Usuario usuario)
        {
            try
            {
                var client = new RestClient("https://localhost:7049/Usuario/Criar");
                var request = new RestRequest("https://localhost:7049/Usuario/Criar", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(usuario), ParameterType.RequestBody);
                var response = client.Execute(request);

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    throw new Exception("Falha ao criar usuário: " + response.Content);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }
    }
}
