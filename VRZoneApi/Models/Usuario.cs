using System.ComponentModel.DataAnnotations;

namespace VRZoneApi.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Rol { get; set; }
    }
}
