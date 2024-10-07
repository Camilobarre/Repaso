using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repaso.Config;
using Repaso.DTOs.Requests;
using Repaso.Models;
using Repaso.Repositories;

namespace Repaso.Controllers.v1.Auth
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        protected readonly IUserRepository _userRepository;

        // Constructor que inyecta el repositorio de usuarios
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Registro de un nuevo usuario
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Verifica si el email ya está registrado
            var existingUser = await _userRepository.GetByEmail(user.Email);
            if (existingUser != null)
                return BadRequest("El email ya está registrado.");

            // Hashea la contraseña del usuario
            user.Password = new JWT().EncryptSHA256(user.Password);

            // Agrega el nuevo usuario a la base de datos
            await _userRepository.AddUser(user);

            return Ok("Usuario registrado exitosamente.");
        }

        // Genera un token JWT para un usuario
        [HttpPost]
        public async Task<IActionResult> GenerateToken(User user)
        {
            var token = JWT.GenerateJwtToken(user);
            return Ok($"ACA ESTA SU TOKEN: {token}");
        }

        // Inicia sesión verificando email y contraseña
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO data)
        {
            var user = await _userRepository.GetByEmail(data.Email);

            if (user == null)
                return BadRequest("El usuario no existe");

            // Verifica si la contraseña es correcta
            if (user.Password != data.Password)
                return BadRequest("Contraseña incorrecta");

            // Verifica si el rol del usuario es admin
            if (user.rol != "admin")
            {
                return Unauthorized("No tiene los permisos necesarios");
            }

            var token = JWT.GenerateJwtToken(user);

            return Ok($"ACA ESTA SU TOKEN: {token}");
        }

        // Obtiene todos los usuarios registrados
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            if (users == null || !users.Any())
            {
                return NoContent();
            }
            return Ok(users);
        }
    }
}