using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRZoneApi.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdEstacion { get; set; }
        public int IdExperiencia { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Estado { get; set; }
        public string Jugadores { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Cliente Cliente { get; set; }
        [ForeignKey(nameof(IdEstacion))]
        public Estacion Estacion { get; set; }
        [ForeignKey(nameof(IdExperiencia))]
        public Experiencia Experiencia { get; set; }
    }
}
