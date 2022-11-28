using ApiUFU.Data;
using ApiUFU.Models;
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
        public IEnumerable<Aluno> PopularBase(int quantidade, string curso)
        {
            PersonNameGenerator geradorDeNomes = new PersonNameGenerator();

            using (var db = new UFUContext())
            {
                Aluno aluno = null;
                for (int i = 0; i < quantidade; i++)
                {
                    aluno.Nome = geradorDeNomes.GenerateRandomFirstAndLastName();
                    aluno.DataNascimento = new DateTime(1996, 6, 6);
                    aluno.
                    db.Alunos.Add(aluno);
                }
                alunos = db.Alunos.ToList();
            }
            

            return alunos;
        }
    }
}
