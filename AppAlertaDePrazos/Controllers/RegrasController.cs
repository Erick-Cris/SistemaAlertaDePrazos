using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace AppAlertaDePrazos.Controllers
{
    public class RegrasController : Controller
    {
        private IConfiguration _configuration;
        public RegrasController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public IActionResult Index()
        {
            string apiToken = HttpContext.Session.GetString("SecurityTokenAPI");
            if (apiToken == null)
                return RedirectToAction("Index", "Login");

            string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
            var client = new RestClient(urlApiAlertaDePrazos);
            var request = new RestRequest("Regras/get", Method.Get);
            request.AddHeader("Authorization", "Bearer " + apiToken);
            List<Regra> regras = client.Execute<List<Regra>>(request).Data;

            ViewBag.Regras = regras;
            return View();
        }

        [Route("Regras/Edit/{regraid}")]
        public IActionResult Edit(int regraid)
        {
            try
            {
                string apiToken = HttpContext.Session.GetString("SecurityTokenAPI");
                if (apiToken == null)
                    return RedirectToAction("Index", "Login");

                string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
                var url = $"{urlApiAlertaDePrazos}/Regras/GetById/?regraid={regraid}";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddHeader("Authorization", "Bearer " + apiToken);
                var response = client.Get(request);
                
                var regra = JsonConvert.DeserializeObject<Regra>(response.Content);

                ViewBag.TokenApi = apiToken;
                ViewBag.Regra = regra;
            }
            catch (Exception e)
            {
                return View();
            }

            return View();
        }

        [HttpPost]
        [Route("Regras/Edit")]
        public IActionResult Edit(Regra regra)
        {
            try
            {
                //var url = $"https://localhost:7049/Regras/Edit";
                //var client = new RestClient(url);
                //var request = new RestRequest();
                //request.AddBody(regra);
                //var response = client.Post(request);
                //if (response.IsSuccessStatusCode)
                //    return Ok();
                //else
                //    throw new Exception("Erro ao atualizar dados: " + response.Content);

                //ViewBag.Regra = regra;

                string apiToken = HttpContext.Session.GetString("SecurityTokenAPI");
                if (apiToken == null)
                    throw new Exception("Se autentique novamente para acessar este recurso.");

                string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
                var client = new RestClient($"{urlApiAlertaDePrazos}/Regras/Edit");
                var request = new RestRequest($"{urlApiAlertaDePrazos}/Regras/Edit", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(regra), ParameterType.RequestBody);
                request.AddHeader("Authorization", "Bearer " + apiToken);
                var response = client.Execute(request);

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    throw new Exception("Erro ao atualizar dados: " + response.Content);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
