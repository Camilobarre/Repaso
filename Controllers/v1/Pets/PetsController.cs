using Microsoft.AspNetCore.Mvc;
using Repaso.Models;
using Repaso.Repositories;

namespace Repaso.Controllers.V1.Pets
{
    [ApiController] // Controlador API
    [Route("api/v1/pets")] // Ruta base
    [ApiExplorerSettings(GroupName = "v1")] // Agrupación para exploración
    public class PetsController : ControllerBase
    {
        protected readonly IPetRepository _petRepository; // Repositorio protegido

        // Constructor que inyecta el repositorio
        public PetsController(IPetRepository petRepository)
        {
            _petRepository = petRepository; // Asigna el repositorio
        }
    }
}
