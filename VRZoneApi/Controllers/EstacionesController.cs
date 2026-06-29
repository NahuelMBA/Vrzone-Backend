using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRZoneApi.Data;
using VRZoneApi.Models;

namespace VRZoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionesController : ControllerBase
    {
        private readonly VRZoneContext _context;

        public EstacionesController(VRZoneContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Estacion>>> GetEstaciones()
        {
            return await _context.Estaciones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estacion>> GetEstacion(int id)
        {
            var estacion = await _context.Estaciones.FindAsync(id);
            if (estacion == null)
            {
                return NotFound();
            }
            return estacion;
        }

        [HttpPost]
        public async Task<ActionResult<Estacion>> CrearEstacion(Estacion estacion)
        {
            _context.Estaciones.Add(estacion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEstacion), new { id = estacion.IdEstacion }, estacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarEstacion(int id, Estacion estacion)
        {
            if (id != estacion.IdEstacion)
            {
                return BadRequest();
            }
            _context.Entry(estacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEstacion(int id)
        {
            var estacion = await _context.Estaciones.FindAsync(id);
            if (estacion == null)
            {
                return NotFound();
            }
            _context.Estaciones.Remove(estacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
