using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repaso.Models;

namespace Repaso.Repositories
{
    public interface IUserRepository
    {
        // Método para agregar un nuevo usuario
        Task AddUser(User user);

        // Método para obtener todos los usuarios
        Task<IEnumerable<User>> GetAll();

        // Método para obtener un usuario por su ID
        Task<User?> GetById(int id);

        // Método para obtener un usuario por su email
        Task<User?> GetByEmail(string email);
    }
}