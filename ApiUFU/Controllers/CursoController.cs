using AlertaDePrazosLibrary.Entities;
using ApiUFU.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CursoController : ControllerBase
    {

        [HttpGet]
        [Route("Get")]
        public IEnumerable<Curso> Get()
        {
            List<Curso> cursos = null;
            using (var db = new UFUContext())
            {
                cursos = db.Cursos.ToList();
            }

            return cursos;
        }

        [HttpPost]
        [Route("SeedCursos")]
        public IActionResult SeedCursos()
        {
            try
            {
                using (var db = new UFUContext())
                {
                    db.Cursos.Add(new Curso() { Titulo = "Sistemas de Informação" });

                    db.SaveChanges();
                }

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            

            return Ok("Cursos populados com sucesso.");
        }
    }
}
