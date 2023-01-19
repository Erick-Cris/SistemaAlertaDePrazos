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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var client = new RestClient("https://localhost:7049");
            var request = new RestRequest("Regras/get", Method.Get);
            List<Regra> regras = client.Execute<List<Regra>>(request).Data;

            client = new RestClient("https://localhost:7149");
            request = new RestRequest("Curso/Get", Method.Get);
            var cursos = client.Execute<List<Curso>>(request).Data;

            Dictionary<Regra, List<Curso>> regrasCursos = new Dictionary<Regra, List<Curso>>();
            ViewBag.Regras = regras;
            foreach(var regra in regras)
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