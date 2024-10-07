using Microsoft.EntityFrameworkCore;
using Repaso.Data;
using Repaso.Models;
using Repaso.Repositories;

namespace Repaso.Services;
public class PetServices : IPetRepository
{
    private readonly ApplicationDbContext _context; // Contexto de la base de datos

    // Constructor que inyecta el contexto
    public PetServices(ApplicationDbContext context)
    {
        _context = context; // Asigna el contexto
    }

    public async Task Add(Pet pet)
    {
        _context.Pets.Add(pet); // Agrega la mascota
        await _context.SaveChangesAsync(); // Guarda cambios
    }

    public async Task<bool> CheckExistence(int id)
    {
        return await _context.Pets.AnyAsync(p => p.Id == id); // Verifica existencia
    }

    public async Task Delete(int id)
    {
        var pet = await GetById(id); // Obtiene la mascota por ID
        if (pet != null)
        {
            _context.Pets.Remove(pet); // Elimina la mascota
            await _context.SaveChangesAsync(); // Guarda cambios
        }
    }

    public async Task<IEnumerable<Pet>> GetAll()
    {
        return await _context.Pets.ToListAsync(); // Obtiene todas las mascotas
    }

    public async Task<Pet?> GetById(int id)
    {
        return await _context.Pets.FindAsync(id); // Busca mascota por ID
    }
}
