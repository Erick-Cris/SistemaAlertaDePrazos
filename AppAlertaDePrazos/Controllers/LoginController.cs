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
        public IActionResult Index()
        {
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

                var url = $"https://localhost:7049/Usuario/Autenticar";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(user), ParameterType.RequestBody);
                var response = client.Execute(request);

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    throw new Exception("Usuário ou senha inválidos.");
            }catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
