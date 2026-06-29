using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRZoneApi.Data;
using VRZoneApi.Dtos;

namespace VRZoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly VRZoneContext _context;

        public UsuariosController(VRZoneContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest datos)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == datos.NombreUsuario && u.Contrasenia == datos.Contrasenia);

            if (usuario == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            return Ok(usuario);
        }
    }
}
