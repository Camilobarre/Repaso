using Microsoft.AspNetCore.Mvc;
using Repaso.Models;
using Repaso.Repositories;

namespace Repaso.Controllers.V1.Pets
{
    [ApiController]
    [Route("api/v1/pets")]
    public class PetsController : ControllerBase
    {
        protected readonly IPetRepository _petRepository;
        public PetsController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }
    
    }
}