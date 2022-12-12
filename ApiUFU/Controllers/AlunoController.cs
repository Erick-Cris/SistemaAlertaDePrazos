using ApiUFU.Data;
using ApiUFU.Models;
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
                        List<Curso> cursos = db.Cursos.ToList();
                        foreach (KeyValuePair<int, List<Disciplina>> curso in dict)
                        {
                            int semestre = 1;
                            for (int i = 1; i <= 140; i++)
                            {

                                if (i == 71) semestre = 6;
                                Aluno aluno = new Aluno();
                                aluno.Id = $"{2015 - k}{semestre}BSI{i}";
                                aluno.CursoId = 1;
                                aluno.Nome = geradorDeNomes.GenerateRandomFirstAndLastName();
                                aluno.DataNascimento = new DateTime(1996, 6, 6);
                                aluno.Email = "erickcristianup@gmail.com";
                                aluno.DataIngresso = new DateTime(k, semestre, 1);
                                


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
                                    db.MatriculaDisciplinas.Add(new MatriculaDisciplina(aluno.Id, disciplina.Id, semestre == 1 ? 1 : 2));
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
    }
}

