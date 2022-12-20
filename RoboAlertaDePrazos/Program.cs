// See https://aka.ms/new-console-template for more information
using AlertaDePrazosLibrary.Entities;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Mail;

try
{
    RestClient _client;
    _client = new RestClient("https://localhost:7149/");
    var request = new RestRequest("Aluno/Get");
    var response = await _client.ExecuteGetAsync(request);
    List<Aluno> alunos = JsonConvert.DeserializeObject<List<Aluno>>(response.Content);

    request = new RestRequest("MatriculaDisciplina/Get");
    response = await _client.ExecuteGetAsync(request);
    List<MatriculaDisciplina> matriculaDisciplinas = JsonConvert.DeserializeObject<List<MatriculaDisciplina>>(response.Content);

    request = new RestRequest("Disciplina/Get");
    response = await _client.ExecuteGetAsync(request);
    List<Disciplina> disciplinas = JsonConvert.DeserializeObject<List<Disciplina>>(response.Content);

    var query = (from a in alunos
                 join md in matriculaDisciplinas on a.Id equals md.AlunoId
                 join d in disciplinas on md.DisciplinaId equals d.Id
                 select new { a, md, d });

    List<int> Semestres = matriculaDisciplinas.Select(x => x.SemestreId).ToList();
    foreach (var semestre  in Semestres)
    {
        Console.WriteLine(semestre);
    }

    //var queryDesempenhoAluno = (from a in alunos
    //                            join md in matriculaDisciplinas on a.Id equals md.AlunoId
    //                            join d in disciplinas on md.DisciplinaId equals d.Id
    //                            select new { a, QtdDisciplinasEliminadas = matriculaDisciplinas.Where(x => x.AlunoId == a.Id).Count(), QtdSemestresCorridos = matriculaDisciplinas.Select(x => x.SemestreId)});


    //foreach (var aluno in query)
    //{
    //    Console.WriteLine($"{aluno.a.Nome}-{aluno.a.Id}-{aluno.d.Titulo}");
    //}

    //MailMessage mail = new MailMessage("erickcristianup@outlook.com", "erickcristianup@gmail.com");
    //SmtpClient client = new SmtpClient();

    //client.EnableSsl = true;
    //client.Host = "smtp.office365.com";
    //client.UseDefaultCredentials = false;
    //client.Credentials = new System.Net.NetworkCredential("erickcristianup@outlook.com", "o]3GY6r/xG]K");

    //client.Port = 587;
    //client.DeliveryMethod = SmtpDeliveryMethod.Network;

    //mail.Subject = "teste";
    //mail.Body = "teste2";

    //client.Send(mail);

    Console.WriteLine("Sucesso");
    while (true) { }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    while (true) { }
}
