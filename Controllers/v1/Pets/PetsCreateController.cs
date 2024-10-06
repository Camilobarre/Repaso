using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repaso.Controllers.V1.Pets;
using Repaso.Models;
using Repaso.Repositories;

namespace Repaso.Controllers.v1.Pets
{
    [ApiController]
    [Route("api/v1/pets")]
    [Tags("pets")]
    public class PetsCreateController : PetsController
    {
        public PetsCreateController(IPetRepository petRepository) : base(petRepository)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPet(Pet pet)
        {
            await _petRepository.Add(pet);
            return Ok("Mascota Creada");
        }
    }
}