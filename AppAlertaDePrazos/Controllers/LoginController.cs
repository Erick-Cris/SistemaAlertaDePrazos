using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using AlertaDePrazosLibrary.Utils;
using System.Security.Cryptography;

namespace AppAlertaDePrazos.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();

            return View();
        }

        [HttpPost]
        public IActionResult Autenticar( string email, string senha)
        {
            try
            {
                string hash = HashHandler.HashPassword(senha);

                Usuario user = new Usuario() { Id = 0, Nome = "", Email = email, IsActive = false, PasswordHash = "" };
                user.PasswordHash = hash;

                string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
                var url = $"{urlApiAlertaDePrazos}/Usuario/Autenticar";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(user), ParameterType.RequestBody);
                var response = client.Execute(request);

                if (response.IsSuccessStatusCode)
                {
                    

                    HttpContext.Session.SetString("SecurityTokenAPI", (string) JsonConvert.DeserializeObject(response.Content));
                    //HttpContext.Session.SetInt32("SessionIdade", 54);

                    return Ok();
                }
                    
                else
                    throw new Exception("Usuário ou senha inválidos.");
            }catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
