using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Enums;
using ApiUFU.Data;
using ApiUFU.Utils;
using Microsoft.AspNetCore.Mvc;
using RandomNameGeneratorLibrary;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        [HttpGet]
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
        public IEnumerable<Aluno> Create()
        {
            List<Aluno> alunos = null;
            using (var db = new UFUContext())
            {
                alunos = db.Alunos.ToList();
            }

            return alunos;
        }

        [HttpPost]
        [Route("SeedAlunos")]
        public IActionResult PopularAlunos()
        {
            try
            {
                PersonNameGenerator geradorDeNomes = new PersonNameGenerator();
                Dictionary<int, List<Disciplina>> dict = new Dictionary<int, List<Disciplina>>();


                using (var db = new UFUContext())
                {
                    Curso cursoss = db.Cursos.ToList().FirstOrDefault();
                    List<Disciplina> disciplinass = db.Disciplinas.ToList();
                    dict.Add(cursoss.Id, disciplinass);


                    //Anos
                    for (int k = 2014; k < DateTime.Now.Year; k++)
                    {
                        Semestre primeiroSemestre = new Semestre() { ano = k, ordem = 1, DataInicio = new DateTime(k, 1, 1), DataFim = new DateTime(k, 6, 15) };
                        Semestre segundoSemestre = new Semestre() { ano = k, ordem = 2, DataInicio = new DateTime(k, 7, 1), DataFim = new DateTime(k, 12, 15) };
                        db.Semestres.Add(primeiroSemestre);
                        db.Semestres.Add(segundoSemestre);
                        db.SaveChanges();

                        List<Curso> cursos = db.Cursos.ToList();
                        foreach (KeyValuePair<int, List<Disciplina>> curso in dict)
                        {
                            bool isPrimeiroSemestre = true;
                            for (int i = 1; i <= 140; i++)
                            {

                                if (i == 71) isPrimeiroSemestre = !isPrimeiroSemestre;
                                Aluno aluno = new Aluno();
                                aluno.Id = $"{2015 - k}{(isPrimeiroSemestre ? primeiroSemestre.ordem : segundoSemestre.ordem)}BSI{i}";
                                aluno.CursoId = 1;
                                aluno.Nome = geradorDeNomes.GenerateRandomFirstAndLastName();
                                aluno.DataNascimento = new DateTime(1996, 6, 6);
                                aluno.Email = "erickcristianup@gmail.com";
                                aluno.DataIngresso = isPrimeiroSemestre ? primeiroSemestre.DataInicio : segundoSemestre.DataInicio;
                                


                                int qtdSemestresCumpridos = DateTime.Now.Month > 5 ? (DateTime.Now.Year - k) * 2 : ((DateTime.Now.Year - k) * 2) - 1;
                                List<Disciplina> disciplinasAluno = new List<Disciplina>();

                                int qtdDiscipilinas = new Random().Next(0, 6);

                                for (int j = 1; j <= qtdSemestresCumpridos; j++)
                                {
                                    if (j <= 8)
                                    {
                                        //Add 5 disciplinas do período atual do aluno
                                        List<Disciplina> disciplinasDoPeriodo = curso.Value.Where(x => ((int)x.Periodo) == j).ToList();
                                        disciplinasDoPeriodo = Functions.ValidaDisciplinas(disciplinasAluno, disciplinasDoPeriodo);


                                        List<Disciplina> disciplinasAprovadas = disciplinasDoPeriodo.OrderBy(r => new Random().Next()).Take(qtdDiscipilinas).ToList();
                                    }
                                    else
                                    {
                                        //Add até 5 disciplinas aleatórias
                                        disciplinasAluno.AddRange(Functions.ValidaDisciplinas(disciplinasAluno, curso.Value.Except(disciplinasAluno).ToList()).OrderBy(r => new Random().Next()).Take(qtdDiscipilinas).ToList());
                                    }
                                }
                                db.Alunos.Add(aluno);
                                db.SaveChanges();
                                foreach (Disciplina disciplina in disciplinasAluno)
                                    db.MatriculaDisciplinas.Add(new MatriculaDisciplina(aluno.Id, disciplina.Id, isPrimeiroSemestre ? primeiroSemestre.Id : segundoSemestre.Id));
                                db.SaveChanges();
                            }
                        }
                    }
                }

                return Ok("Base populada com sucesso!");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("SeedAlunosV2")]
        public IActionResult PopularAlunosV2()
        {
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
                    

                    var anoInicio = 2014;

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
                                aluno.Id = $"{semestre.ano - (anoInicio - 1)}{semestre.ordem}BSI{i}";
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

