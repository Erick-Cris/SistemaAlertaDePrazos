// See https://aka.ms/new-console-template for more information
using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using AlertaDePrazosLibrary.Enums;
using Newtonsoft.Json;
using RestSharp;
using RoboAlertaDePrazos;
using System.Net;
using System.Net.Mail;



try
{
    ClientUFU _clientUFU = new ClientUFU();
    ClientSistemaAlertaDePrazos _clientSistemaAlertaDePrazos = new ClientSistemaAlertaDePrazos();

    //Dados UFU
    Semestre semestreAtual = await _clientUFU.GetSemestreAtual();
    List<Semestre> semestres = await _clientUFU.GetSemestres();
    List<Estagio> estagios = await _clientUFU.GetEstagiosEmAberto();
    List<Aluno> alunos = await _clientUFU.GetAlunos();
    List<MatriculaDisciplina> matriculaDisciplinas = await _clientUFU.GetMatriculaDisciplinas();
    List<Disciplina> disciplinas = await _clientUFU.GetDisciplinas();

    //Dados Sistema de Alerta
    List<Regra> regras = await _clientSistemaAlertaDePrazos.GetRegras();

    List<int> Semestres = matriculaDisciplinas.Select(x => x.SemestreId).ToList();

    foreach (Aluno aluno in alunos)
    {
        List<MatriculaDisciplina> matriculaDisciplinasAluno = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id).ToList();
        List<Disciplina> disciplinasObrigatoriasDoCurso = disciplinas.Where(x => x.CursoId == aluno.CursoId).ToList();
        List<Disciplina> disciplinasEliminadas = disciplinasObrigatoriasDoCurso.Where(x => matriculaDisciplinas.Any(y => y.AlunoId == aluno.Id && y.Nota >= 60 && y.DisciplinaId == x.Id)).ToList();

        //Verificar Desempenho
        int qtdDisciplinasEliminadas = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id && x.Nota >= 60).Count();
        int semestresCorridos = (DateTime.Now.Year - aluno.DataIngresso.Year) * 2;
        if (aluno.DataIngresso.Month > 4) semestresCorridos = semestresCorridos - 1;
        if (DateTime.Now.Month < 4) semestresCorridos = semestresCorridos - 1;
        semestresCorridos = semestresCorridos - 1;//Para desconsiderar as disciplinas em andamento

        int qtdDisciplinasPendentes = 40 - qtdDisciplinasEliminadas;
        int semestresRemanescentes = 12 - semestresCorridos;
        if (qtdDisciplinasPendentes > semestresRemanescentes * 5)
        {
            Alerta("rendimento", aluno, null, null, regras);
        }



        //Verificar se só se matriculou em 2 disciplinas
        if (semestreAtual != null)
        {
            List<MatriculaDisciplina> disciplinasAlunoSemestre = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id && x.SemestreId == semestreAtual.Id).ToList();
            if (disciplinasAlunoSemestre.Count < 3)
                AlertaTrancamentoParcial(aluno);
        }

        //Verificar Estagios
        Estagio estagioEmAndamento = estagios.Where(x => x.Status == StatusEstagio.EmAndamento && x.AlunoId == aluno.Id).ToList().FirstOrDefault();

        if (estagioEmAndamento != null)
        {
            if (estagioEmAndamento.Tipo == TipoEstagio.Obrigatorio)
                Alerta("estagio obrigatório ativo", aluno, null, null, regras);
            else
                Alerta("estagio não obrigatório ativo", aluno, null, null, regras);
        }
        else
        {
            if (disciplinasEliminadas.Count >= 25)
                Alerta("estagio obrigatório possível", aluno, null, null, regras);
            else
                if (disciplinasEliminadas.Where(x => x.Periodo == Periodo.Primeiro).ToList().Count == 5 && disciplinasEliminadas.Where(x => x.Periodo == Periodo.Segundo).ToList().Count == 5)
                Alerta("estagio não obrigatório possível", aluno, null, null, regras);
        }


        //Trancamento Parcial
        List<MatriculaDisciplina> matriculaDisciplinasTrancadas = matriculaDisciplinas.Where(x => x.Trancamento == true && x.AlunoId == aluno.Id).ToList();
        List<Disciplina> disciplinasTrancadas = disciplinas.Where(x => x.CursoId == aluno.CursoId && matriculaDisciplinasTrancadas.Any(y => y.DisciplinaId == x.Id)).ToList();
        if (disciplinasTrancadas.Count > 0)
            Alerta("trancamento parical ativo", aluno, disciplinasTrancadas, null, regras);
        else
            Alerta("trancamento parcial passivo", aluno, null, null, regras);

        //Trancamento Geral
        List<Semestre> semestresTrancadosAluno = new List<Semestre>();
        List<int> semestresIdCursadosAluno = matriculaDisciplinasAluno.Select(x => x.SemestreId).ToList();
        List<Semestre> semestresDesdeIngressoAluno = semestres.Where(x => x.DataInicio >= aluno.DataIngresso).ToList();
        foreach (Semestre semestre in semestresDesdeIngressoAluno)
        {
            if (semestresIdCursadosAluno.Where(x => x == semestre.Id).ToList().Count > 0)
                semestresTrancadosAluno.Add(semestre);
        }
        if (semestresTrancadosAluno.Count > 0)
            Alerta("trancamento geral ativo", aluno, null, semestresTrancadosAluno, regras);
        Alerta("trancamento geral passivo", aluno, null, null, regras);
    }

    Console.WriteLine("Sucesso");
    while (true) { }
}
catch (Exception e)
{
    Console.WriteLine($"[ERRO] {e.Message} {e.StackTrace}");
    while (true) { }
}

#region Alertas

static void Alerta(string tipoRegra, Aluno aluno, List<Disciplina> disciplinas, List<Semestre> semestres, List<Regra> regras)
{
    Regra regra = regras.Where(x => x.Nome.ToLower() == tipoRegra).FirstOrDefault();
    if(regra != null && regra.IsActive)
    {
        switch (tipoRegra.ToLower())
        {
            case "rendimento": AlertaDilacaoDePrazo(aluno); break;
            case "trancamento parical ativo": AlertaTrancamentoParcialAtiva(aluno, disciplinas); break;
            case "trancamento geral ativo": AlertaTrancamentoGeralAtivo(aluno, semestres); break;
            case "trancamento parcial passivo": AlertaTrancamentoParcialPassivo(aluno); break;
            case "trancamento geral passivo": AlertaTrancamentoGeralPassivo(aluno); break;
            case "estagio obrigatório possível": AlertaPossivelEstagioObrigatorio(aluno); break;
            case "estagio não obrigatório possível": AlertaPossivelEstagioNaoObrigatorio(aluno); break;
            case "estagio obrigatório ativo": AlertaEstagioObrigatorio(aluno); break;
            case "estagio não obrigatório ativo": AlertaEstagioNaoObrigatorio(aluno); break;
        }
    }
}


static void AlertaTrancamentoParcial(Aluno aluno)
{
    Console.WriteLine($"[Trancamento Parcial] {aluno.Id}");
    string assunto = "[UFU] Alerta de matrículas em disciplinas";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Foi identificado que você está matriculado em 2 componentes curriculares nesse semestre.</p>
                      <p>Se atente a norma da instituição que define que um aluno deve se matricuar em no mínimo 2 disciplinas.</p>
                      <p>Logo, sugerimos que procure aumentar o número de componentes curriculares neste semestre por meio do Reajuste de disciplinas no Portal do Aluno.</p>
                      <p>Norma:
                        <ul style='font-size: 1.5rem'>
                        <li> Art. 111. Não poderá ser concedido trancamento parcial que resulte em vinculação inferior a 2 (dois) componentes curriculares do curso. </li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Trancamento Parcial] Falha ao enviar e-mail.");
}

static void AlertaDilacaoDePrazo(Aluno aluno)
{
    Console.WriteLine($"[Dilação de Prazo] {aluno.Id}");
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
                      <p>É imporante se atentar também ao prazo para solicitação de dilação conforme previsto na Norma de graduação abaixo: 
                        <ul>
                            <li>Art. 174. § 1- O estudante poderá solicitar dilação de prazo antes de transcorridos 3/4 do período letivo.</li>
                        </ul>  
                      </p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Dilação de Prazo] Falha ao enviar e-mail.");
}
static void AlertaTrancamentoParcialAtiva(Aluno aluno, List<Disciplina> disciplinas)
{
    Console.WriteLine($"[Trancamento Parcial Ativa] {aluno.Id}");
    string disciplinasTrancadas = String.Empty;
    foreach (Disciplina disciplina in disciplinas)
    {
        disciplinasTrancadas += $"<li> [{disciplina.Id}] {disciplina.Titulo}</li>";
    }

    string assunto = "[UFU] Alerta de Trancamento Parcial";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Foi identificado que você está já utilizou o recurso de trancamento parcial nos seguintes componentes curriculares do seu curso:
                        <ul>
                            " + disciplinasTrancadas + @"
                        </ul></p>
                      <p>Se atente as restrições presentes nas Normas da Graduação que se aplicam sobre o Trancamento Parcial.</p>
                      <p>.</p>
                      <p>Normas da Graduação:
                        <ul style='font-size: 1.5rem'>
                        <li> Art. 110. O trancamento parcial de matrícula pode ser concedido, uma única vez, em cada componente curricular, devendo ser solicitado antes de decorrido 1/3 do semestre ou ano letivo, conforme prazo previsto no Calendário Acadêmico, desde que sejam atendidas estas Normas e os regulamentos específicos do curso. </li>
                        <li> Art. 92. § 5- A matrícula nos componentes curriculares de que trata este artigo será automaticamente cancelada caso o estudante solicite trancamento parcial de matrícula e fique com carga horária inferior a 120 (cento e vinte) horas semestrais em componentes curriculares no seu curso de origem. </li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Trancamento Parcial Ativa] Falha ao enviar e-mail.");
}
static void AlertaTrancamentoParcialPassivo(Aluno aluno)
{
    Console.WriteLine($"[Trancamento Parical Passivo] {aluno.Id}");
    string assunto = "[UFU] Alerta de Trancamento Parcial";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>O trancamento de disciplinas é um recurso que só pode ser contemplado se respeitando os limites e prazos previstos nas normas da graduação.
                      <br/>
                      <p>Normas da Graduação:
                        <ul style='font-size: 1.5rem'>
                        <li> Art. 110. O trancamento parcial de matrícula pode ser concedido, uma única vez, em cada componente curricular, devendo ser solicitado antes de decorrido 1/3 do semestre ou ano letivo, conforme prazo previsto no Calendário Acadêmico, desde que sejam atendidas estas Normas e os regulamentos específicos do curso. </li>
                        <li> Art. 92. § 5- A matrícula nos componentes curriculares de que trata este artigo será automaticamente cancelada caso o estudante solicite trancamento parcial de matrícula e fique com carga horária inferior a 120 (cento e vinte) horas semestrais em componentes curriculares no seu curso de origem. </li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Trancamento Parical Passivo] Falha ao enviar e-mail.");
}
static void AlertaEstagioObrigatorio(Aluno aluno)
{
    Console.WriteLine($"[Estagio Obrigatorio] {aluno.Id}");
    string assunto = $"[UFU] Alerta de prazo de Estágio Obrigatório";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Foi identificado que você possui um Estágio Obrigatório em andamento.</p>
                      <p>Para garantir o aproveitamento deste componente, mantenha comunicação com seu Professor Orientador e que entregue o Relatório de Atividades ao fim do estágio.</p>
                      <p>Gostaríamos também de Alerta-lo de que para ser aprovado neste componente curricular é necessário que o discente se atente aos prazos previstos nas Normas de Estágio.</p>
                      <p>Normas:
                        <ul style='font-size: 1.5rem'>
                        <li> Art. 5 – O estágio curricular obrigatório terá carga horária de 440 horas de atividades, cumpridas dentro de um período mínimo de 16 semanas e máximo de 32 semanas. </li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Estagio Obrigatorio] Falha ao enviar e-mail.");
}
static void AlertaEstagioNaoObrigatorio(Aluno aluno)
{
    Console.WriteLine($"[Estágio Não Obrigatório] {aluno.Id}");
    string assunto = $"[UFU] Alerta de prazo de Estágio Não Obrigatório";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Foi identificado que você possui um Estágio Não Obrigatório em andamento.</p>
                      <p>Para garantir o aproveitamento deste componente, mantenha comunicação com seu Professor Orientador e que entregue o Relatório de Atividades ao fim do estágio.</p>
                      <p>Gostaríamos também de Alerta-lo de que para ser aprovado neste componente curricular é necessário que o discente se atente aos prazos previstos nas Normas de Estágio.</p>
                      <p>Normas:
                        <ul style='font-size: 1.5rem'>
                        <li> Art. 6 – O estágio curricular não obrigatório terá carga horária mínima de 220 horas de atividades, cumpridas dentro de um período mínimo de 8 semanas e máximo de 24 semanas, sem limite na quantidade de estágios a não ser aqueles previstos pela Lei de Estágio. </li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Estágio Não Obrigatório] Falha ao enviar e-mail.");
}
static void AlertaPossivelEstagioObrigatorio(Aluno aluno)
{
    Console.WriteLine($"[Estágio Obrigatório Disponível] {aluno.Id}");
    string assunto = "[UFU] Alerta de Estágio Obrigatório Disponível";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Identificamos que você já atende aos pré requisitos previstos nas Normas de Estágios para poder dar início ao estágio Obrigatório.</p>
                      <p>O estágio obrigatório é um componente curricular obrigatório para a conclusão do curso.</p>
                      <p>Logo, a Coordenação recomenda que se organize para eliminar este concorrente o quanto antes, para evitar futuros contratempos na sua formação.</p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Estágio Obrigatório Disponível] Falha ao enviar e-mail.");
}
static void AlertaPossivelEstagioNaoObrigatorio(Aluno aluno)
{
    Console.WriteLine($"[Estágio Não Obrigatório Disponível] {aluno.Id}");
    string assunto = "[UFU] Alerta de Estágio NÃO Obrigatório Disponível";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Identificamos que você já atende aos pré requisitos previstos nas Normas de Estágios para poder dar início ao estágio Não Obrigatório.</p>
                      <p>Essa modalidade de estágio não é obrigatória para realizar a conclusão do curso, mas permite estabelecer contratos formais de estágio que contabilizarão horas complementares ao discente.</p>
                      <p>Lembramos que para a conclusão do curso, é necessário possuir um determinado saldo de horas complementares.</p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Estágio Não Obrigatório Disponível] Falha ao enviar e-mail.");
}
static void AlertaTrancamentoGeralAtivo(Aluno aluno, List<Semestre> semestres)
{
    Console.WriteLine($"[Trancamento Geral Ativo] {aluno.Id}");
    string assunto = "[UFU] Alerta de Trancamento Geral";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>Foi identificado que sua matrícula consumiu o recursto de Trancamento Geral de semestre.</p>
                      <p>Fique atento aos limites impostos pelas normas do curso em relação ao Trancamento Geral.</p>
                      <p>Norma da Graduação:
                        <ul style='font-size: 1.5rem'>
                        <li> Art. 102. O trancamento geral de matrícula será efetuado por, no máximo, 2 (dois) semestres letivos, consecutivos ou não, para os cursos semestrais, ou por 1 (um) ano, para os cursos anuais.</li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Trancamento Geral Ativo] Falha ao enviar e-mail.");
}
static void AlertaTrancamentoGeralPassivo(Aluno aluno)
{
    Console.WriteLine($"[Trancamento Geral Passivo] {aluno.Id}");
    string assunto = "[UFU] Alerta de Trancamento Geral";
    string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Caro Aluno.</p>
                      <p>É importante que o discente se conscientise sobre os limites previstos nas Normas da Graduação a respeito do recurso de Trancamento Geral do Semestre.</p>
                      <p>Normas da Graduação:
                        <ul style='font-size: 1.5rem'>
                        <li> Art. 100. Não serão concedidos trancamentos geral e parcial de matrícula ao estudante que estiver:
                            <ul>
                                <li> III – em dilação de prazo para integralização curricular.</li>
                            </ul>
                            Parágrafo único. O Colegiado de Curso poderá conceder trancamento por motivo de força maior ou caso fortuito nos casos que se enquadram nos incisos II e III.
                        </li>
                        <li>Art. 104. É vedado o trancamento geral no primeiro ano letivo para os cursos anuais e nos 2 (dois) primeiros semestres letivos para os cursos semestrais da UFU, exceto por motivo de caso fortuito ou de força maior.</li>
                        </ul></p>
                        <br/><br/>
                      <p>Atenciosamente,<br>Coordenação.</br></p>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";
    if (!EnviarEmail(assunto, body, ""))
        throw new Exception("[Trancamento Geral Passivo] Falha ao enviar e-mail.");
}
#endregion

static bool EnviarEmail(string assunto, string corpo, string destinatario)
{
    try
    {
        //MailMessage mail = new MailMessage("erickcristianup@outlook.com", "erickcristianup@gmail.com");
        //SmtpClient client = new SmtpClient();

        //client.EnableSsl = true;
        //client.Host = "smtp.office365.com";
        //client.UseDefaultCredentials = false;
        //client.Credentials = new System.Net.NetworkCredential("erickcristianup@outlook.com", "o]3GY6r/xG]K");

        //client.Port = 587;
        //client.DeliveryMethod = SmtpDeliveryMethod.Network;

        //mail.Subject = assunto;
        //mail.Body = corpo;
        //mail.IsBodyHtml = true;

        //client.Send(mail);

        return true;
    }
    catch (Exception e)
    {
        return false;
    }
}