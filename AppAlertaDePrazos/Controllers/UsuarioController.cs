using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using AlertaDePrazosLibrary.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;

namespace AppAlertaDePrazos.Controllers
{
    public class UsuarioController : Controller
    {
        private IConfiguration _configuration;
        public UsuarioController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [Route("Usuario/Ativar/{token}")]
        public IActionResult Ativar(string token)
        {
            //Redireciona para tela de criação de senhas
            //Entrada: Usuário clica no link de ativação enviado para sua caixa de e-mail
            try
            {
                string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
                var url = $"{urlApiAlertaDePrazos}/Usuario/Ativar/?token={token}";
                var client = new RestClient(url);
                var request = new RestRequest();
                var response = client.Get(request);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Usuario = JsonConvert.DeserializeObject<Usuario>(response.Content);
                    return View("CriarSenha");
                }
                else
                    return RedirectToAction("Index","Login");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Login");
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
                string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
                var client = new RestClient($"{urlApiAlertaDePrazos}/Usuario/Criar");
                var request = new RestRequest($"{urlApiAlertaDePrazos}/Usuario/Criar", Method.Post);
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

        [HttpGet]
        [Route("Usuario/CriarSenha")]
        public IActionResult CriarSenha()
        {
            return View();
        }

        [HttpPost]
        [Route("Usuario/CriarSenha")]
        public IActionResult CriarSenha(Usuario usuario)
        {
            try
            {
                string hash = HashHandler.HashPassword(usuario.PasswordHash);

                Usuario user = usuario;
                user.PasswordHash = hash;
                user.IsActive = true;

                string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
                var client = new RestClient($"{urlApiAlertaDePrazos}/Usuario/Editar");
                var request = new RestRequest($"{urlApiAlertaDePrazos}/Usuario/Editar", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(usuario), ParameterType.RequestBody);
                var response = client.Execute(request);

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                    throw new Exception("Falha ao criar senha: " + response.Content);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
