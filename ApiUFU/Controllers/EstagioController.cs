using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Enums;
using ApiUFU.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstagioController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("Get")]
        public IActionResult Get()
        {
            try
            {
                List<Estagio> estagios = null;
                using (var db = new UFUContext())
                {
                    estagios = db.Estagios.ToList();
                }

                return Ok(estagios);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }

        [HttpGet]
        [Authorize]
        [Route("GetEstagiosEmAberto")]
        public IActionResult GetEstagiosEmAberto()
        {
            try
            {
                List<Estagio> estagios = null;
                using (var db = new UFUContext())
                {
                    estagios = db.Estagios.Where(x => x.Status == StatusEstagio.EmAndamento).ToList();
                }

                return Ok(estagios);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
