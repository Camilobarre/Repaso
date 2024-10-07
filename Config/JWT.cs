using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Repaso.Models;

namespace Repaso.Config
{
    public class JWT
    {
        // Genera un token JWT basado en los datos del usuario
        public static string GenerateJwtToken(User user)
        {
            // Crea las claims (información del usuario) que se incluirán en el token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Obtiene las variables del entorno necesarias para la configuración del JWT
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var jwtExpiresIn = Environment.GetEnvironmentVariable("JWT_EXPIRES_IN");

            // Valida que todas las variables del entorno necesarias están configuradas
            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
            {
                throw new InvalidOperationException("JWT configuration values are missing.");
            }

            // Crea una clave simétrica a partir del valor de `jwtKey`
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            // Establece las credenciales de firma utilizando el algoritmo HMAC-SHA256
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crea el token JWT con el emisor, audiencia, claims y tiempo de expiración
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtExpiresIn)),
                signingCredentials: credentials
            );

            // Devuelve el token generado como una cadena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Método que encripta una cadena utilizando el algoritmo SHA-256
        public string EncryptSHA256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convierte el input en bytes y calcula el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convierte el hash a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                // Retorna la cadena encriptada
                return builder.ToString();
            }
        }
    }
}
