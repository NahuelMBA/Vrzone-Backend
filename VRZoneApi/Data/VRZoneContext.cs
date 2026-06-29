using Microsoft.EntityFrameworkCore;
using VRZoneApi.Models;

namespace VRZoneApi.Data
{
    public class VRZoneContext : DbContext
    {
        public VRZoneContext(DbContextOptions<VRZoneContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estacion> Estaciones { get; set; }
        public DbSet<Experiencia> Experiencias { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { IdUsuario = 1, NombreUsuario = "admin", Contrasenia = "admin", Rol = "Administrador" },
                new Usuario { IdUsuario = 2, NombreUsuario = "recepcion", Contrasenia = "1234", Rol = "Recepcionista" }
            );

            modelBuilder.Entity<Estacion>().HasData(
                new Estacion { IdEstacion = 1, Nombre = "Estacion 1", Descripcion = "Puesto con visor Oculus Quest 2", Activa = true },
                new Estacion { IdEstacion = 2, Nombre = "Estacion 2", Descripcion = "Puesto con visor HTC Vive", Activa = true }
            );

            modelBuilder.Entity<Experiencia>().HasData(
                new Experiencia { IdExperiencia = 1, Nombre = "Beat Saber", Descripcion = "Juego de ritmo con sables de luz", DuracionMinutos = 30, Genero = "Musical", EdadMinima = 7 },
                new Experiencia { IdExperiencia = 2, Nombre = "Horror VR", Descripcion = "Experiencia de terror", DuracionMinutos = 45, Genero = "Terror", EdadMinima = 16 }
            );
        }
    }
}
