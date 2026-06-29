using System.ComponentModel.DataAnnotations;

namespace VRZoneApi.Models
{
    public class Estacion
    {
        [Key]
        public int IdEstacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activa { get; set; }
    }
}
