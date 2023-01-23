using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using AlertaDePrazosLibrary.Enums;
using Newtonsoft.Json;
using RoboAlertaDePrazos;
using System.Net.Mail;

try
{
    Console.WriteLine("<======================== INICIO DA EXECUÇÃO ============================>");

    //Clients de acesso a APIs
    ClientUFU _clientUFU = new ClientUFU();
    ClientSistemaAlertaDePrazos _clientSistemaAlertaDePrazos = new ClientSistemaAlertaDePrazos();

    //Dados UFU
    Semestre semestreAtual = await _clientUFU.GetSemestreAtual();
    List<Semestre> semestres = await _clientUFU.GetSemestres();
    List<Estagio> estagios = await _clientUFU.GetEstagiosEmAberto();
    List<Aluno> alunos = await _clientUFU.GetAlunos();
    List<MatriculaDisciplina> matriculaDisciplinas = await _clientUFU.GetMatriculaDisciplinas();
    List<Disciplina> disciplinas = await _clientUFU.GetDisciplinas();
    List<Alerta> alertas = await _clientSistemaAlertaDePrazos.GetAlertas();

    //Dados Sistema de Alerta
    List<Regra> regras = await _clientSistemaAlertaDePrazos.GetRegras();

    foreach (Aluno aluno in alunos)
    {
        try
        {
            List<MatriculaDisciplina> matriculaDisciplinasAluno = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id).ToList();
            List<Disciplina> disciplinasObrigatoriasDoCurso = disciplinas.Where(x => x.CursoId == aluno.CursoId).ToList();
            List<Disciplina> disciplinasEliminadas = disciplinasObrigatoriasDoCurso.Where(x => matriculaDisciplinas.Any(y => y.AlunoId == aluno.Id && y.Nota >= 60 && y.DisciplinaId == x.Id)).ToList();

            //Regra Verificar Desempenho do Disciente [Sistemas de Informação - Santa Mônica]
            int qtdDisciplinasEliminadas = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id && x.Nota >= 60).Count();
            int semestresCorridos = (DateTime.Now.Year - aluno.DataIngresso.Year) * 2;
            if (aluno.DataIngresso.Month > 4) semestresCorridos = semestresCorridos - 1;
            if (DateTime.Now.Month < 4) semestresCorridos = semestresCorridos - 1;
            semestresCorridos = semestresCorridos - 1;//Para desconsiderar as disciplinas em andamento
            int qtdDisciplinasPendentes = 40 - qtdDisciplinasEliminadas;
            int semestresRemanescentes = 12 - semestresCorridos;
            if (qtdDisciplinasPendentes > semestresRemanescentes * 5)
            {
                Alerta("rendimento", aluno, null, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);
            }

            //Regra Aluno se matriculou apenas em 2 disciplinas [Sistemas de Informação - Santa Mônica]
            if (semestreAtual != null)
            {
                List<MatriculaDisciplina> disciplinasAlunoSemestre = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id && x.SemestreId == semestreAtual.Id).ToList();
                if (disciplinasAlunoSemestre.Count < 3)
                    AlertaTrancamentoParcial(aluno);
            }

            //Regra verificar Estagios [Sistemas de Informação - Santa Mônica]
            Estagio estagioEmAndamento = estagios.Where(x => x.Status == StatusEstagio.EmAndamento && x.AlunoId == aluno.Id).ToList().FirstOrDefault();
            if (estagioEmAndamento != null)
            {
                if (estagioEmAndamento.Tipo == TipoEstagio.Obrigatorio)
                    Alerta("estagio obrigatório ativo", aluno, null, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);
                else
                    Alerta("estagio não obrigatório ativo", aluno, null, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);
            }
            else
            {
                if (disciplinasEliminadas.Count >= 25)
                    Alerta("estagio obrigatório possível", aluno, null, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);
                else
                    if (disciplinasEliminadas.Where(x => x.Periodo == Periodo.Primeiro).ToList().Count == 5 && disciplinasEliminadas.Where(x => x.Periodo == Periodo.Segundo).ToList().Count == 5)
                    Alerta("estagio não obrigatório possível", aluno, null, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);
            }


            //Regra Trancamento Parcial [Sistemas de Informação - Santa Mônica]
            List<MatriculaDisciplina> matriculaDisciplinasTrancadas = matriculaDisciplinas.Where(x => x.Trancamento == true && x.AlunoId == aluno.Id).ToList();
            List<Disciplina> disciplinasTrancadas = disciplinas.Where(x => x.CursoId == aluno.CursoId && matriculaDisciplinasTrancadas.Any(y => y.DisciplinaId == x.Id)).ToList();
            if (disciplinasTrancadas.Count > 0)
                Alerta("trancamento parical ativo", aluno, disciplinasTrancadas, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);
            else
                Alerta("trancamento parcial passivo", aluno, null, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);

            //Regra Trancamento Geral [Sistemas de Informação - Santa Mônica]
            List<Semestre> semestresTrancadosAluno = new List<Semestre>();
            List<int> semestresIdCursadosAluno = matriculaDisciplinasAluno.Select(x => x.SemestreId).ToList();
            List<Semestre> semestresDesdeIngressoAluno = semestres.Where(x => x.DataInicio >= aluno.DataIngresso).ToList();
            foreach (Semestre semestre in semestresDesdeIngressoAluno)
            {
                if (semestresIdCursadosAluno.Where(x => x == semestre.Id).ToList().Count > 0)
                    semestresTrancadosAluno.Add(semestre);
            }
            if (semestresTrancadosAluno.Count > 0)
                Alerta("trancamento geral ativo", aluno, null, semestres, semestresTrancadosAluno, regras, alertas, _clientSistemaAlertaDePrazos);
            Alerta("trancamento geral passivo", aluno, null, semestres, null, regras, alertas, _clientSistemaAlertaDePrazos);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERRO][ALUNO: {aluno.Id}] {ex.Message} {ex.StackTrace}");
        }
        
    }

    Console.WriteLine("");
    Console.WriteLine("<======================== FIM DA EXECUÇÃO ============================>");
    while (true) { /*Manter console aberto após fim da execução*/}
}
catch (Exception e)
{
    Console.WriteLine($"[ERRO][CRÍTICO] {e.Message} {e.StackTrace}");
    while (true) { /*Manter console aberto após fim da execução*/ }
}

//Método de disparo de Alertas.
//Encapsula o disparo de todos os Alertas.
static void Alerta(string tipoRegra, Aluno aluno, List<Disciplina> disciplinas, List<Semestre> semestres, List<Semestre> semestresTrancadosPorAluno, List<Regra> regras, List<Alerta> alertas, ClientSistemaAlertaDePrazos _client)
{
    Regra regra = regras.Where(x => x.Nome.ToLower() == tipoRegra).FirstOrDefault();
    int[] cursoIdList = JsonConvert.DeserializeObject<int[]>(regra.Parametros);

    Alerta alertaMaisRecente = alertas.Where(x => x.MatriculaAluno == aluno.Id && x.RegraId == regra.Id).OrderByDescending(x => x.DataAlerta).ToList().FirstOrDefault();
    bool flagAlertaDisponivel = VerificaDisponibilidadeAlerta(semestres, alertaMaisRecente);

    if (regra != null && regra.IsActive && cursoIdList.Contains(aluno.CursoId) && regra.IsActive && flagAlertaDisponivel)
    {
        switch (tipoRegra.ToLower())
        {
            case "rendimento": AlertaDilacaoDePrazo(aluno); break;
            case "trancamento parical ativo": AlertaTrancamentoParcialAtiva(aluno, disciplinas); break;
            case "trancamento geral ativo": AlertaTrancamentoGeralAtivo(aluno, semestresTrancadosPorAluno); break;
            case "trancamento parcial passivo": AlertaTrancamentoParcialPassivo(aluno); break;
            case "trancamento geral passivo": AlertaTrancamentoGeralPassivo(aluno); break;
            case "estagio obrigatório possível": AlertaPossivelEstagioObrigatorio(aluno); break;
            case "estagio não obrigatório possível": AlertaPossivelEstagioNaoObrigatorio(aluno); break;
            case "estagio obrigatório ativo": AlertaEstagioObrigatorio(aluno); break;
            case "estagio não obrigatório ativo": AlertaEstagioNaoObrigatorio(aluno); break;
        }

        Alerta alerta = new Alerta() { MatriculaAluno = aluno.Id, RegraId = regra.Id, DataAlerta = DateTime.Now};
        if (!_client.CriarAlerta(alerta))
            throw new Exception($"Falha ao criar Alerta. Aluno: {aluno.Id}, Regra: {regra.Id}, Data/Hora: {DateTime.Now}");
        else
            alertas.Add(alerta);
    }
}

//Métodos de disparo de e-mail.
//Encapsula Mensagem e disparo de e-mail do alerta.
//Sua descrição pode ser lida no corpo do e-mail do método.
#region Alertas
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
        throw new Exception($"[ERRO][Trancamento Parcial][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Dilação de Prazo][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Trancamento Parcial Ativa][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Trancamento Parical Passivo][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Estagio Obrigatorio][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Estágio Não Obrigatório][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Estágio Obrigatório Disponível][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Estágio Não Obrigatório Disponível][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Trancamento Geral Ativo][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
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
        throw new Exception($"[ERRO][Trancamento Geral Passivo][Aluno: {aluno.Id}] Falha ao enviar e-mail.");
}
#endregion

//Verifica se o alerta já foi enviado, para evitar SPAM.
static bool VerificaDisponibilidadeAlerta(List<Semestre> semestres, Alerta alerta)
{
    Semestre semestreAtual = semestres.Where(x => x.DataInicio <= DateTime.Now && x.DataFim >= DateTime.Now).ToList().FirstOrDefault();
    Semestre ultimoSemestreCorrido = semestres.Where(x => x.DataFim < DateTime.Now).OrderByDescending(x => x.DataFim).ToList().FirstOrDefault();

    if (alerta != null)//Já existe um alerta desse tipo disparado para o aluno
    {
        if (semestreAtual == null)//Execução está ocorreno fora de um semestre (nas férias)
        {
            if (alerta.DataAlerta < ultimoSemestreCorrido.DataInicio)//Alerta já ocorreu antes do último semestre corrido
                return true;
            else if (alerta.DataAlerta >= ultimoSemestreCorrido.DataInicio && alerta.DataAlerta <= ultimoSemestreCorrido.DataFim)//Alerta ocorreu dentro do último semestre corrido
                return true;
            else
                return false;//Alerta já ocorreu após o último semestre (nas férias)
        }
        else
        {
            if (alerta.DataAlerta < semestreAtual.DataInicio)//Alerta ocorreu antes do semestre atual
                return true;
            else if (alerta.DataAlerta >= semestreAtual.DataInicio && alerta.DataAlerta <= semestreAtual.DataFim)//Alerta já ocorreu durante o semestre atual
                return false;
            else
                throw new Exception($"Erro de lógica ao validar disponibilidade de envio do alerta.");//Alerta já ocorreu depois do semestre atual (não deve ser possível)
        }
    }
    else
        return true;//Ainda não foi disparado esse tipo de alerta para o aluno

}

//Método que dispara o e-mail por meio de um servidor de e-mails SMTP da Outlook.
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
    catch (Exception e)
    {
        return false;
    }
}