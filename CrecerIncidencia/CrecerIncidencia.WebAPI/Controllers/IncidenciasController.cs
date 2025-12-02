using CrecerIncidencia.Application;
using CrecerIncidencia.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CrecerIncidencia.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidenciasController : ControllerBase
    {
        private readonly IIncidenciaService _service;

        public IncidenciasController(IIncidenciaService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Crear ([FromBody] IncidenciaRequestDTO request)
        {
            var id = await _service.CrearIncidenciaAsync(request);

            var incidenciaCreada = await _service.ObtenerPorIdAsync(id);

            return Ok(incidenciaCreada);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var inc = await _service.ObtenerPorIdAsync(id);
            if (inc == null) return NotFound();
            return Ok(inc);
        }

        [HttpPut("{id:int}/estado")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoRequestDTO request)
        {
            var ok = await _service.ActualizarEstadoAsync(id, request);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos([FromServices] IIncidenciaService service)
        {
            var incidencias = await _service.ObtenerTodosAsync();

            return Ok(incidencias);
        }

    }
}