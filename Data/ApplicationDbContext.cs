using Microsoft.EntityFrameworkCore;
using Repaso.Models;

namespace Repaso.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}