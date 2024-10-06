using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repaso.Controllers.V1.Pets;
using Repaso.Repositories;

namespace Repaso.Controllers.v1.Pets
{
    [ApiController]
    [Route("api/v1/pets")]
    [Tags("pets")]
    public class PetsGetController : PetsController
    {
        public PetsGetController(IPetRepository petRepository) : base(petRepository)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _petRepository.GetAll();
            if (pets == null || !pets.Any())
            {
                return NoContent();
            }
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _petRepository.GetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }
    }
}