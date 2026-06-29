using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRZoneApi.Data;
using VRZoneApi.Models;

namespace VRZoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly VRZoneContext _context;

        public PagosController(VRZoneContext context)
        {
            _context = context;
        }

        [HttpGet("reserva/{idReserva}")]
        public async Task<ActionResult<List<Pago>>> GetPagosPorReserva(int idReserva)
        {
            return await _context.Pagos
                .Where(p => p.IdReserva == idReserva)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> RegistrarPago(Pago pago)
        {
            var reserva = await _context.Reservas.FindAsync(pago.IdReserva);
            if (reserva == null)
            {
                return NotFound("No existe la reserva");
            }

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return Ok(pago);
        }
    }
}
