using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repaso.Controllers.V1.Pets;
using Repaso.Repositories;

namespace Repaso.Controllers.v1.Pets
{
    [ApiController] // Indica que esta clase es un controlador API
    [Route("api/v1/pets")] // Define la ruta base para el controlador
    [Tags("pets")] // Asocia este controlador con la etiqueta "pets" para la documentación
    [ApiExplorerSettings(GroupName = "v1")] // Configura la exploración de la API para el grupo "v1"
    public class PetsDeleteController : PetsController // Hereda de PetsController
    {
        // Constructor que inyecta IPetRepository, permitiendo la inyección de dependencias
        public PetsDeleteController(IPetRepository petRepository) : base(petRepository)
        {
        }

        // Método para eliminar una mascota, accesible a través de DELETE
        [HttpDelete("{id}")] // Define el método HTTP y la ruta con el parámetro id
        [Authorize] // Requiere autenticación para acceder a este método
        public async Task<IActionResult> DeletePet(int id)
        {
            await _petRepository.Delete(id); // Llama al método Delete del repositorio
            return NoContent(); // Devuelve un estado 204 No Content si la eliminación es exitosa
        }
    }
}
