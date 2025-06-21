using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionDeFlotas.Modelos;

namespace GestionDeFlotas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginDtoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginDtoesController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/LoginDtoes/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            
            // Verificar si el usuario existe en la base de datos
            var usuario = await _context.Usuario
                .Where(u => u.User == loginDto.Usuario)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return Unauthorized("Usuario no existe o  es incorrecto");
            }

            // Validar la contraseña con Bcrypt
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.Password))
            {
                return Unauthorized("Contraseña incorrecta");
            }

            return Ok(usuario); 
        }

    }
}
