using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Enums;
using ApiUFU.Data;
using ApiUFU.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomNameGeneratorLibrary;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("Get")]
        public IEnumerable<Aluno> Get()
        {
            List<Aluno> alunos = null;
            using (var db = new UFUContext())
            {
                alunos = db.Alunos.ToList();
            }

            return alunos;
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<Aluno> Create()
        {
            List<Aluno> alunos = null;
            using (var db = new UFUContext())
            {
                alunos = db.Alunos.ToList();
            }

            return alunos;
        }

        [HttpGet]
        [Authorize]
        [Route("SeedAlunosV2")]
        public IActionResult PopularAlunosV2()
        {
            //Popula Base da UFU
            //Adiciona Semestres, alunos e estágios para uma quantidad.
            try
            {
                PersonNameGenerator geradorDeNomes = new PersonNameGenerator();
                Dictionary<int, List<Disciplina>> dict = new Dictionary<int, List<Disciplina>>();
                Dictionary<int, List<Semestre>> anoSemestre = new Dictionary<int, List<Semestre>>();
                List<MatriculaDisciplina> matriculaDisciplinas = new List<MatriculaDisciplina>();
                List<Aluno> alunos = new List<Aluno>();
                List<Estagio> estagios = new List<Estagio>();

                using (var db = new UFUContext())
                {
                    List<Semestre> semestres = new List<Semestre>();
                    List<Curso> cursos = db.Cursos.ToList();
                    estagios = db.Estagios.ToList();

                    foreach(Curso curso in cursos)
                    {
                        dict.Add(curso.Id, db.Disciplinas.Where(x => x.CursoId == curso.Id).ToList());
                    }
                    

                    var anoInicio = 2014;//Ano de início para criação de semestres.

                    //Ano
                    for (int k = anoInicio; k < DateTime.Now.Year; k++)
                    {
                        //Semestres
                        Semestre semestre = null;
                        for (int smstr = 1; smstr <= 2; smstr++)
                        {
                            semestre = new Semestre() { ano = k, ordem = smstr, DataInicio = new DateTime(k, smstr < 2 ? 1 : 6, 1), DataFim = new DateTime(k, smstr < 2 ? 6 : 12, 15) };
                            db.Semestres.Add(semestre);
                            semestres.Add(semestre);
                        }
                        db.SaveChanges();
                    }

                    semestres = semestres.OrderBy(x => x.ano).ThenBy(x => x.ordem).ToList();

                    //Alunos
                    foreach (Semestre semestre in semestres)
                    {
                        //Cursos
                        foreach (KeyValuePair<int, List<Disciplina>> curso in dict)
                        {
                            for (int i = 1; i <= 70; i++)
                            {
                                Aluno aluno = new Aluno();
                                aluno.Id = $"{semestre.ano - (anoInicio - 1)}{semestre.ordem}{curso.Key}BSI{i}";
                                aluno.CursoId = curso.Key;
                                aluno.Nome = geradorDeNomes.GenerateRandomFirstAndLastName();
                                aluno.DataNascimento = new DateTime(1996, 6, 6);
                                aluno.Email = "erickcristianup@gmail.com";
                                aluno.DataIngresso = semestre.DataInicio;

                                alunos.Add(aluno);
                            }
                        }
                    }
                    db.Alunos.AddRange(alunos);
                    db.SaveChanges();
                    alunos = db.Alunos.ToList();

                    foreach(Curso curso in cursos)
                    {
                        foreach (Semestre semestre in semestres)
                        {
                            List<Aluno> alunosMatriculadosNoCurso = alunos.Where(x => x.CursoId == curso.Id).ToList();

                            foreach (Aluno aluno in alunosMatriculadosNoCurso)
                            {

                                //Inicio Estágio Não Obrigatório
                                bool estagioEmAndamento = false;
                                if (semestre.Id == semestres[semestres.Count - 1].Id || semestre.Id == semestres[semestres.Count - 2].Id)
                                    estagioEmAndamento = true;
                                Estagio estagio = Functions.AlunoFazEstagio(aluno, matriculaDisciplinas, dict.Where(x => x.Key == curso.Id).FirstOrDefault().Value, estagios, semestre, curso, estagioEmAndamento);
                                if (estagio != null)
                                    estagios.Add(estagio);
                                //Fim Estágio Não Obrigatório

                                int quantidadeSemestresAluno = semestres.Where(x => x.DataInicio >= aluno.DataIngresso && x.DataInicio <= semestre.DataInicio).ToList().Count;
                                
                                //Trancamento Geral
                                if(new Random().Next(1, 11) < 9)
                                {
                                    if (quantidadeSemestresAluno <= 8)
                                    {
                                        //Add 5 disciplinas do período atual do aluno
                                        List<Disciplina> disciplinasDoPeriodo = dict.Where(x => x.Key == aluno.CursoId).FirstOrDefault().Value.Where(x => ((int)x.Periodo) == quantidadeSemestresAluno).ToList();
                                        disciplinasDoPeriodo = Functions.ValidaDisciplinasV2(aluno, matriculaDisciplinas, disciplinasDoPeriodo);

                                        int qtdDiscipilinas = new Random().Next(0, 6);
                                        List<Disciplina> disciplinasAprovadas = disciplinasDoPeriodo.OrderBy(r => new Random().Next()).Take(qtdDiscipilinas).ToList();
                                        foreach (Disciplina disciplina in disciplinasAprovadas)
                                        {
                                            matriculaDisciplinas.Add(new MatriculaDisciplina(aluno.Id, disciplina.Id, semestre.Id, new Random().Next(60, 101)));
                                        }
                                        List<Disciplina> disciplinasReprovadas = disciplinasDoPeriodo.Except(disciplinasAprovadas).ToList();
                                        foreach (Disciplina disciplina in disciplinasReprovadas)
                                        {
                                            //Trancamento Parcial
                                            if(new Random().Next(0, 100) > 65)
                                                matriculaDisciplinas.Add(new MatriculaDisciplina(aluno.Id, disciplina.Id, semestre.Id, new Random().Next(0, 60)));
                                            else
                                                matriculaDisciplinas.Add(new MatriculaDisciplina(aluno.Id, disciplina.Id, semestre.Id, 0, true));
                                        }
                                    }
                                    else
                                    {

                                        List<MatriculaDisciplina> matriculaDisciplinaAlunoAprovadas = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id && x.Nota >= 60).ToList();
                                        List<Disciplina> disciplinasAlunoAprovadas = dict.Where(x => x.Key == curso.Id).FirstOrDefault().Value.Where(x => matriculaDisciplinaAlunoAprovadas.Any(y => y.DisciplinaId == x.Id)).ToList();
                                        List<Disciplina> disciplinaPendentes = dict.Where(x => x.Key == curso.Id).FirstOrDefault().Value.Except(disciplinasAlunoAprovadas).ToList();


                                        //Add até 5 disciplinas aleatórias
                                        int qtdDiscipilinas = new Random().Next(0, 6);
                                        List<Disciplina> disciplinasAprovadas = Functions.ValidaDisciplinasV2(aluno, matriculaDisciplinas, disciplinaPendentes).OrderBy(r => new Random().Next()).Take(qtdDiscipilinas).ToList();
                                        foreach (Disciplina disciplina in disciplinasAprovadas)
                                        {
                                            matriculaDisciplinas.Add(new MatriculaDisciplina(aluno.Id, disciplina.Id, semestre.Id, new Random().Next(0, 60)));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    db.Estagios.AddRange(estagios);
                    db.MatriculaDisciplinas.AddRange(matriculaDisciplinas);
                    db.SaveChanges();
                }

                return Ok("Base populada com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}

