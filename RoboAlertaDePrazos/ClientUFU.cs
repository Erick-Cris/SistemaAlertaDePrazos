using AlertaDePrazosLibrary.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboAlertaDePrazos
{
    public class ClientUFU
    {
        public RestClient _client;

        public ClientUFU()
        {
            _client = new RestClient("https://localhost:7149/");
        }

        public async Task<Semestre> GetSemestreAtual()
        {
            Semestre semestreAtual = null;
            var request = new RestRequest("Semestre/GetSemestreAtual");
            var response = await _client.ExecuteGetAsync(request);
            semestreAtual = JsonConvert.DeserializeObject<Semestre>(response.Content);
            return semestreAtual;
        }

        public async Task<List<Semestre>> GetSemestres()
        {
            var request = new RestRequest("Semestre/Get");
            var response = await _client.ExecuteGetAsync(request);
            List<Semestre> semestres = JsonConvert.DeserializeObject<List<Semestre>>(response.Content);
            return semestres;
        }

        public async Task<List<Estagio>> GetEstagiosEmAberto()
        {
            var request = new RestRequest("Estagio/GetEstagiosEmAberto");
            var response = await _client.ExecuteGetAsync(request);
            List<Estagio> estagios = JsonConvert.DeserializeObject<List<Estagio>>(response.Content);

            return estagios;
        }

        public async Task<List<Aluno>> GetAlunos()
        {
            var request = new RestRequest("Aluno/Get");
            var response = await _client.ExecuteGetAsync(request);
            List<Aluno> alunos = JsonConvert.DeserializeObject<List<Aluno>>(response.Content);
            return alunos;
        }

        public async Task<List<MatriculaDisciplina>> GetMatriculaDisciplinas()
        {
            var request = new RestRequest("MatriculaDisciplina/Get");
            var response = await _client.ExecuteGetAsync(request);
            List<MatriculaDisciplina> matriculaDisciplinas = JsonConvert.DeserializeObject<List<MatriculaDisciplina>>(response.Content);
            return matriculaDisciplinas;
        }

        public async Task<List<Disciplina>> GetDisciplinas()
        {
            var request = new RestRequest("Disciplina/Get");
            var response = await _client.ExecuteGetAsync(request);
            List<Disciplina> disciplinas = JsonConvert.DeserializeObject<List<Disciplina>>(response.Content);

            return disciplinas;
        }
    }
}
