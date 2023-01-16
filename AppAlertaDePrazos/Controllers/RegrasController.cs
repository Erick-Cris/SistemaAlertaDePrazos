using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace AppAlertaDePrazos.Controllers
{
    public class RegrasController : Controller
    {
        public IActionResult Index()
        {
            var client = new RestClient("https://localhost:7049");
            var request = new RestRequest("Regras/get", Method.Get);
            List<Regra> regras = client.Execute<List<Regra>>(request).Data;

            ViewBag.Regras = regras;
            return View();
        }

        [Route("Regras/Edit/{regraid}")]
        public IActionResult Edit(int regraid)
        {
            try
            {
                var url = $"https://localhost:7049/Regras/GetById/?regraid={regraid}";
                var client = new RestClient(url);
                var request = new RestRequest();
                var response = client.Get(request);
                var regra = JsonConvert.DeserializeObject<Regra>(response.Content);

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

                var client = new RestClient("https://localhost:7049/Regras/Edit");
                var request = new RestRequest("https://localhost:7049/Regras/Edit", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(regra), ParameterType.RequestBody);
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
