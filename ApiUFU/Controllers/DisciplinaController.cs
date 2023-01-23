using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Enums;
using ApiUFU.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinaController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("Get")]
        public IEnumerable<Disciplina> Get()
        {
            List<Disciplina> disciplinas = null;
            using (var db = new UFUContext())
            {
                disciplinas = db.Disciplinas.ToList();
            }

            return disciplinas;
        }
    }
}
