using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRZoneApi.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public int IdReserva { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string Medio { get; set; }

        [ForeignKey(nameof(IdReserva))]
        public Reserva Reserva { get; set; }
    }
}
