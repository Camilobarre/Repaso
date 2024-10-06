using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repaso.Config;
using Repaso.Models;

namespace Repaso.Controllers.v1.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
         // metodo que me genera un JWT
    [HttpPost]
    public async Task<IActionResult> GenerateToken(User user)
    {
        var token = JWT.GenerateJwtToken(user);
        return Ok($"ACA ESTA SU TOKEN: {token}");
    }

    }
}