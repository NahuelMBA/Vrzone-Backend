using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRZoneApi.Data;

namespace VRZoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly VRZoneContext _context;

        public ReportesController(VRZoneContext context)
        {
            _context = context;
        }

        [HttpGet("experiencias-mas-reservadas")]
        public async Task<IActionResult> ExperienciasMasReservadas()
        {
            var resultado = await _context.Reservas
                .Where(r => r.Estado != "Cancelada")
                .GroupBy(r => r.Experiencia.Nombre)
                .Select(g => new { Experiencia = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .ToListAsync();

            return Ok(resultado);
        }

        [HttpGet("reservas-por-periodo")]
        public async Task<IActionResult> ReservasPorPeriodo(DateTime desde, DateTime hasta)
        {
            var cantidad = await _context.Reservas
                .Where(r => r.Fecha >= desde && r.Fecha <= hasta && r.Estado != "Cancelada")
                .CountAsync();

            return Ok(new { Desde = desde, Hasta = hasta, Cantidad = cantidad });
        }
    }
}
