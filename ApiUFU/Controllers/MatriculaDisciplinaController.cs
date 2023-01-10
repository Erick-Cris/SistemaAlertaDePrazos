using AlertaDePrazosLibrary.Entities;
using ApiUFU.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatriculaDisciplinaController : ControllerBase
    {
        [HttpGet]
        [Route("Get")]
        public IEnumerable<MatriculaDisciplina> Get()
        {
            List<MatriculaDisciplina> matriculaDisciplinas = null;
            using (var db = new UFUContext())
            {
                matriculaDisciplinas = db.MatriculaDisciplinas.ToList();
            }

            return matriculaDisciplinas;
        }
    }
}
