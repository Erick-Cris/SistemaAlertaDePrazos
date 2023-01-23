using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using AppAlertaDePrazos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;

namespace AppAlertaDePrazos.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        public HomeController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public IActionResult Index()
        {
            string apiToken = HttpContext.Session.GetString("SecurityTokenAPI");
            if(apiToken == null)
                return RedirectToAction("Index", "Login");

            ////Busca regras
            string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
            var client = new RestClient(urlApiAlertaDePrazos);
            var request = new RestRequest("Regras/get", Method.Get);
            request.AddHeader("Authorization", "Bearer " + apiToken);
            var response = client.Execute(request);
            List<Regra> regras = JsonConvert.DeserializeObject<List<Regra>>(response.Content);

            //Busca Cursos da FACOM
            string urlApiUfu = _configuration["Configuracoes:UrlApiUFU"];
            client = new RestClient(urlApiUfu);
            request = new RestRequest("Curso/Get", Method.Get);
            request.AddHeader("Authorization", "Bearer " + apiToken);
            response = client.Execute(request);
            List<Curso> cursos = JsonConvert.DeserializeObject<List<Curso>>(response.Content);

            //Prepara dados para serem dispostos no front end, na tela de regras
            Dictionary<Regra, List<Curso>> regrasCursos = new Dictionary<Regra, List<Curso>>();
            ViewBag.Regras = regras;
            ViewBag.TokenApi = apiToken;
            foreach (var regra in regras)
            {
                int[] cursoIdList = JsonConvert.DeserializeObject<int[]>(regra.Parametros);
                if(cursoIdList.Length > 0)
                {
                    regrasCursos.Add(regra, cursos.Where(x => cursoIdList.Any(y => y == x.Id)).ToList());
                }

            }
            ViewBag.RegrasCursos = regrasCursos;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}