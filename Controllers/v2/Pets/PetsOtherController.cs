using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repaso.Controllers.V1.Pets;
using Repaso.Repositories;

namespace Repaso.Controllers.v2.Pets
{
    [ApiController]
    [Route("api/petsOthers")]
    [ApiExplorerSettings(GroupName ="v2")]
    public class PetsOtherController : PetsController
    {
        public PetsOtherController(IPetRepository petRepository) : base(petRepository)
        {
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePet(int id)
        {
            await _petRepository.Delete(id);
            return NoContent();
        }
    }
}