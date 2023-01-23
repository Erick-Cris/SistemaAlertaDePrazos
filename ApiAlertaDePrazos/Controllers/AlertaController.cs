using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using ApiAlertaDePrazos.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlertaDePrazos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertaController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("Get")]
        public IActionResult Get()
        {
            try
            {
                List<Alerta> alertas = null;
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    alertas = db.Alertas.ToList();
                }

                return Ok(alertas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public IActionResult Create(Alerta alerta)
        {
            try
            {
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    db.Alertas.Add(alerta);
                    db.SaveChanges();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
