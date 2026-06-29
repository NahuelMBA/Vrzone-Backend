using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRZoneApi.Data;
using VRZoneApi.Models;

namespace VRZoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly VRZoneContext _context;

        public ReservasController(VRZoneContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reserva>>> GetReservas(string estado)
        {
            var consulta = _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Estacion)
                .Include(r => r.Experiencia)
                .AsQueryable();

            if (!string.IsNullOrEmpty(estado))
            {
                consulta = consulta.Where(r => r.Estado == estado);
            }

            return await consulta.OrderBy(r => r.Fecha).ThenBy(r => r.HoraInicio).ToListAsync();
        }

        [HttpGet("turnos-disponibles")]
        public async Task<ActionResult<List<string>>> GetTurnosDisponibles(string fecha, int estacionId)
        {
            var dia = DateTime.Parse(fecha);
            var reservas = await _context.Reservas
                .Where(r => r.IdEstacion == estacionId
                    && r.Fecha == dia
                    && r.Estado != "Cancelada")
                .ToListAsync();

            var disponibles = new List<string>();
            for (int hora = 9; hora < 21; hora++)
            {
                var inicioTurno = new TimeSpan(hora, 0, 0);
                var finTurno = inicioTurno.Add(TimeSpan.FromHours(1));

                bool ocupado = false;
                foreach (var r in reservas)
                {
                    var inicio = TimeSpan.Parse(r.HoraInicio);
                    var fin = TimeSpan.Parse(r.HoraFin);
                    if (inicioTurno < fin && finTurno > inicio)
                    {
                        ocupado = true;
                        break;
                    }
                }

                if (!ocupado)
                {
                    disponibles.Add(hora.ToString("D2") + ":00");
                }
            }
            return disponibles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Estacion)
                .Include(r => r.Experiencia)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }
            return reserva;
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> CrearReserva(Reserva reserva)
        {
            if (TimeSpan.Parse(reserva.HoraFin) <= TimeSpan.Parse(reserva.HoraInicio))
            {
                return BadRequest("La hora de fin debe ser posterior a la de inicio");
            }
            if (HaySolapamiento(reserva))
            {
                return Conflict("La estación ya tiene una reserva en ese horario");
            }

            reserva.Estado = "Confirmada";
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReserva), new { id = reserva.IdReserva }, reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarReserva(int id, Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return BadRequest();
            }
            if (TimeSpan.Parse(reserva.HoraFin) <= TimeSpan.Parse(reserva.HoraInicio))
            {
                return BadRequest("La hora de fin debe ser posterior a la de inicio");
            }
            if (HaySolapamiento(reserva))
            {
                return Conflict("La estación ya tiene una reserva en ese horario");
            }
            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            reserva.Estado = "Cancelada";
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool HaySolapamiento(Reserva reserva)
        {
            var reservasDelDia = _context.Reservas
                .Where(r => r.IdEstacion == reserva.IdEstacion
                    && r.Fecha == reserva.Fecha
                    && r.Estado != "Cancelada"
                    && r.IdReserva != reserva.IdReserva)
                .ToList();

            var inicioNueva = TimeSpan.Parse(reserva.HoraInicio);
            var finNueva = TimeSpan.Parse(reserva.HoraFin);

            foreach (var r in reservasDelDia)
            {
                var inicio = TimeSpan.Parse(r.HoraInicio);
                var fin = TimeSpan.Parse(r.HoraFin);
                if (inicioNueva < fin && finNueva > inicio)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
