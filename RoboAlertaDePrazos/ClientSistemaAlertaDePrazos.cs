using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboAlertaDePrazos
{
    public class ClientSistemaAlertaDePrazos
    {
        public RestClient _client;

        public ClientSistemaAlertaDePrazos()
        {
            _client = new RestClient("https://localhost:7049/");
        }

        public async Task<List<Regra>> GetRegras()
        {
            List<Regra> regras = null;
            var request = new RestRequest("Regras/Get");
            var response = await _client.ExecuteGetAsync(request);
            regras = JsonConvert.DeserializeObject<List<Regra>>(response.Content);
            return regras;
        }

    }
}
