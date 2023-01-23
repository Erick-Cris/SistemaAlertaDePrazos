using AlertaDePrazosLibrary.Entities;
using ApiUFU.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SemestreController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("Get")]
        public IEnumerable<Semestre> Get()
        {
            List<Semestre> semestres = null;
            using (var db = new UFUContext())
            {
                semestres = db.Semestres.ToList();
            }

            return semestres;
        }

        [HttpGet]
        [Authorize]
        [Route("GetSemestreAtual")]
        public IActionResult GetSemestreAtual()
        {
            try
            {
                List<Semestre> semestres = null;
                using (var db = new UFUContext())
                {
                    //Erick
                    //semestres = db.Semestres.Where(x => x.DataInicio < DateTime.Now && x.DataFim > DateTime.Now).ToList();
                    semestres = db.Semestres.Where(x => x.Id == 18).ToList();
                }

                return Ok(semestres.FirstOrDefault());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
