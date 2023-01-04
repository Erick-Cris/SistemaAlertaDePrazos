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

    var count = 0;
    foreach (Aluno aluno in alunos)
    {
        int qtdDisciplinasEliminadas = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id && x.Nota >= 60).Count();
        int semestresCorridos = (DateTime.Now.Year - aluno.DataIngresso.Year) *2;
        if (aluno.DataIngresso.Month > 4) semestresCorridos = semestresCorridos - 1;
        if (DateTime.Now.Month < 4) semestresCorridos = semestresCorridos - 1;
        semestresCorridos = semestresCorridos - 1;//Para desconsiderar as disciplinas em andamento

        int qtdDisciplinasPendentes = 40 - qtdDisciplinasEliminadas;
        int semestresRemanescentes = 12 - semestresCorridos;
        if(qtdDisciplinasPendentes > semestresRemanescentes * 5)
        {
            AlertaDilacaoDePrazo(aluno);
            count++;
            //Console.WriteLine($"[Aluno]: {aluno.Nome}, [Matricula]: {aluno.Id} está com baixon rendimento.[{count}]");

            if(count == 6)break;
        }
    }

    //TesteEmail();

    //foreach (var aluno in query)
    //{
    //    Console.WriteLine($"{aluno.a.Nome}-{aluno.a.Id}-{aluno.d.Titulo}");
    //}



    Console.WriteLine("Sucesso");
    while (true) { }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    while (true) { }
}

static void AlertaDilacaoDePrazo(Aluno aluno)
{
    string assunto = "[UFU] Alerta de Baixo Rendimento";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Seu rendimento acadêmico atual está próximo de atingir um nível <b>CRÍTICO</b>, devido ao tempo limite disponível para a conclusão do curso estar próximo de ser insuficiente para eliminar o volume de disciplinas pendentes.</p>
                      <p>Recomendamos que se organize com antecedência para esse tema pois excedido o tempo limite pode resultar em jubilamento.</p>
                      <p>Caso o atinja o tempo limite, será necessário solicitar dilação do prazo para o aluno que atenda a um dos seguintes requisitos:
                        <ul style='font-size: 1.5rem'>
                        <li> Quando, pelo menos, 80% (oitenta por cento) da carga horária total da integralização curricular fixada no Projeto Pedagógico do Curso estiver concluída.</li>
                        <li> Quando faltar apenas cumprir o Estágio Obrigatório, Trabalho de Conclusão de Curso, Monografia.</li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (EnviarEmail(assunto, body, ""))
        Console.WriteLine("Teste de envio de e-mai: [SUCESSO]");
    else
        Console.WriteLine("Teste de envio de e-mai: [FALHA]");
}

static bool EnviarEmail(string assunto, string corpo, string destinatario)
{
    try
    {
        MailMessage mail = new MailMessage("erickcristianup@outlook.com", "erickcristianup@gmail.com");
        SmtpClient client = new SmtpClient();

        client.EnableSsl = true;
        client.Host = "smtp.office365.com";
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("erickcristianup@outlook.com", "o]3GY6r/xG]K");

        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;

        mail.Subject = assunto;
        mail.Body = corpo;
        mail.IsBodyHtml = true;

        client.Send(mail);

        return true;
    }
    catch(Exception e)
    {
        return false;
    }
}

static void TesteEmail()
{

    string body = @"<html>
                      <body>
                      <p>Caro Aluno</p>
                      <p>Seu rendimento acadêmico atual está próximo de atingir um nível crítico, devido ao tempo limite disponível para a conclusão do curso estar próximo de ser insuficiente para eliminar o volume de disciplinas pendentes.</p>
                      <p>Recomendamos atenção urgente a esse tema pois excedido o tempo limite pode resultar em jubilamento</p>
                      <p>Caso o atinja o tempo limite, será possível solicitar dilação do prazo caso o aluno atenda aos seguintes rqeuisitos:</p>

                      <p>Atenciosamente,<br>-Jack</br></p>
                      </body>
                      </html>
                     ";
    if(EnviarEmail("Teste", body, ""))
        Console.WriteLine("Teste de envio de e-mai: [SUCESSO]");
    else
        Console.WriteLine("Teste de envio de e-mai: [FALHA]");

}