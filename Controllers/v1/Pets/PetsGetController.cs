using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repaso.Controllers.V1.Pets;
using Repaso.Repositories;

namespace Repaso.Controllers.v1.Pets
{
    // Define un controlador de API para manejar solicitudes relacionadas con mascotas
    [ApiController]
    [Route("api/v1/pets")]  // Establece la ruta base para las solicitudes
    [Tags("pets")]  // Añade una etiqueta descriptiva para la documentación
    [ApiExplorerSettings(GroupName = "v1")]  // Especifica que este controlador pertenece a la versión v1 de la API
    public class PetsGetController : PetsController
    {
        // Constructor que inicializa el repositorio de mascotas inyectado
        public PetsGetController(IPetRepository petRepository) : base(petRepository)
        {
        }

        // Método HTTP GET para obtener todas las mascotas
        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _petRepository.GetAll();  // Recupera todas las mascotas del repositorio
            if (pets == null || !pets.Any())  // Verifica si no hay mascotas
            {
                return NoContent();  // Responde con 204 No Content si no se encuentran mascotas
            }
            return Ok(pets);  // Devuelve una respuesta 200 OK con la lista de mascotas
        }

        // Método HTTP GET para obtener una mascota por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _petRepository.GetById(id);  // Busca la mascota por su ID
            if (pet == null)  // Verifica si la mascota no existe
            {
                return NotFound();  // Responde con 404 Not Found si no se encuentra
            }
            return Ok(pet);  // Devuelve una respuesta 200 OK con la mascota encontrada
        }
    }
}
