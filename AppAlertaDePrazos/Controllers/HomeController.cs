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
            //Busca regras
            string urlApiAlertaDePrazos = _configuration["Configuracoes:UrlApiAlertaDePrazos"];
            var client = new RestClient(urlApiAlertaDePrazos);
            var request = new RestRequest("Regras/get", Method.Get);
            List<Regra> regras = client.Execute<List<Regra>>(request).Data;

            //Busca Cursos da FACOM
            string urlApiUfu = _configuration["Configuracoes:UrlApiUFU"];
            client = new RestClient(urlApiUfu);
            request = new RestRequest("Curso/Get", Method.Get);
            var cursos = client.Execute<List<Curso>>(request).Data;

            //Prepara dados para serem dispostos no front end, na tela de regras
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