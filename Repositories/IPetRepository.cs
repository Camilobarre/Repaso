using Repaso.Models;

namespace Repaso.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetById(int id);
        Task Add(Pet pet);
        Task Delete(int id);
        Task<bool> CheckExistence(int id);
    }
}