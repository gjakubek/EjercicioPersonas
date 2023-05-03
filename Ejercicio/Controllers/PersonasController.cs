using Ejercicio.Entities;
using Ejercicio.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ejercicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonasController : ControllerBase
    {
        private IPersonasRepository _personasRepository;
        public PersonasController(IPersonasRepository personasRepository)
        {
            _personasRepository = personasRepository;
        }
        [HttpGet]
        [Route("people")]
        public async Task<ActionResult> GetAll()
        {            
            try
            {
                var personas = await _personasRepository.GetAll();
                return Ok(personas);
            }
            catch (System.Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Al obtener el listado de personas"
                });
            }
        }
        [HttpGet]
        [Route("people/shuffle")]
        public async Task<ActionResult> GetRandom()
        {
            try
            {
                var personas = await _personasRepository.Randome();
                return Ok(personas);
            }
            catch (System.Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Al obtener un resulta aleatoreo"
                });
            }
        }
        [HttpGet]
        [Route("people/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var persona = await _personasRepository.GetById(id);
                return Ok(persona);
            }
            catch (System.Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener la perosna con id: " + id });
            }
        }
        [HttpDelete]
        [Route("people/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resp = await _personasRepository.Delete(id);
                return Ok(resp);

            }
            catch (System.Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener la perosna con id: " + id });
            }
        }
    }
}
