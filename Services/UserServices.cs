using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repaso.Data;
using Repaso.Models;
using Repaso.Repositories;

namespace Repaso.Services
{
    public class UserServices : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor que inyecta el contexto de la base de datos
        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los usuarios
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        // Método para obtener un usuario por su email
        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Método para obtener un usuario por su ID
        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Método para agregar un nuevo usuario
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}