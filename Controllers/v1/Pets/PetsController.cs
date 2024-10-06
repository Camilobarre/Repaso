using Microsoft.AspNetCore.Mvc;
using Repaso.Models;
using Repaso.Repositories;

namespace Repaso.Controllers.V1.Pets
{
    [ApiController]
    [Route("api/v1/pets")]
    public class PetsController : ControllerBase
    {
        private readonly IPetRepository _petRepository;
        public PetsController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _petRepository.GetAll();
            if (pets == null || pets.Any())
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

        [HttpPost]
        public async Task<IActionResult> AddPet(Pet pet)
        {
            await _petRepository.Add(pet);
            return Ok("Mascota Creada");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            await _petRepository.Delete(id);
            return NoContent();
        }
    
    }
}