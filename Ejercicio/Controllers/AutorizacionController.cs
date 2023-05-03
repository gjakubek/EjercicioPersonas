using Ejercicio.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ejercicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacionController : ControllerBase
    {
        private readonly IGeneradorToken _generadorToken;
        public AutorizacionController(IGeneradorToken generadorToken)
        {
            _generadorToken = generadorToken;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
                var vigencia = (DateTime.Now.AddMinutes(30) - DateTime.Now).TotalHours;
            try
            {
                var retorno = _generadorToken.TokenApis(vigencia);
                return Ok(retorno);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }        
    }
}
