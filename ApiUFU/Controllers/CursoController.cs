using AlertaDePrazosLibrary.Entities;
using ApiUFU.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CursoController : ControllerBase
    {

        [HttpGet]
        [Authorize]
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
    }
}
