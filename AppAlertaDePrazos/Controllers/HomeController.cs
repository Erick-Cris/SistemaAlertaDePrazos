using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using AppAlertaDePrazos.Models;
using Microsoft.AspNetCore.Mvc;
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

            ViewBag.Regras = regras;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}