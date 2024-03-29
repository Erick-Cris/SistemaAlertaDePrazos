﻿using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using ApiAlertaDePrazos.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlertaDePrazos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegrasController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("Get")]
        public IActionResult Get()
        {
            try
            {
                List<Regra> regras = null;
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    regras = db.Regras.ToList();
                }

                return Ok(regras);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetById")]
        public IActionResult GetById(int regraid)
        {
            try
            {
                Regra regra = null;
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    regra = db.Regras.Where(x => x.Id == regraid).ToList().FirstOrDefault();
                }

                return Ok(regra);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Edit")]
        public IActionResult Edit( Regra regra)
        {
            try
            {
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    db.Regras.Update(regra);
                    db.SaveChanges();
                }

                return Ok(regra);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
