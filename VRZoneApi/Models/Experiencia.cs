using System.ComponentModel.DataAnnotations;

namespace VRZoneApi.Models
{
    public class Experiencia
    {
        [Key]
        public int IdExperiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public string Genero { get; set; }
        public int EdadMinima { get; set; }
    }
}
