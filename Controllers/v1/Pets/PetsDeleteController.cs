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
    public class PetsDeleteController : PetsController
    {
        public PetsDeleteController(IPetRepository petRepository) : base(petRepository)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            await _petRepository.Delete(id);
            return NoContent();
        }
    }
}