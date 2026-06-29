using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRZoneApi.Data;
using VRZoneApi.Models;

namespace VRZoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienciasController : ControllerBase
    {
        private readonly VRZoneContext _context;

        public ExperienciasController(VRZoneContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Experiencia>>> GetExperiencias()
        {
            return await _context.Experiencias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Experiencia>> GetExperiencia(int id)
        {
            var experiencia = await _context.Experiencias.FindAsync(id);
            if (experiencia == null)
            {
                return NotFound();
            }
            return experiencia;
        }

        [HttpPost]
        public async Task<ActionResult<Experiencia>> CrearExperiencia(Experiencia experiencia)
        {
            _context.Experiencias.Add(experiencia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExperiencia), new { id = experiencia.IdExperiencia }, experiencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarExperiencia(int id, Experiencia experiencia)
        {
            if (id != experiencia.IdExperiencia)
            {
                return BadRequest();
            }
            _context.Entry(experiencia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarExperiencia(int id)
        {
            var experiencia = await _context.Experiencias.FindAsync(id);
            if (experiencia == null)
            {
                return NotFound();
            }
            _context.Experiencias.Remove(experiencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
