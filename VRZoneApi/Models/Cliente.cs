using System.ComponentModel.DataAnnotations;

namespace VRZoneApi.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int CantidadPersonas { get; set; }
        public string Jugadores { get; set; }
    }
}
