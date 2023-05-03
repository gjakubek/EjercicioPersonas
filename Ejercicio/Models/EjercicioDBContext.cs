using Ejercicio.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ejercicio.Models
{
    public class EjercicioDBContext : DbContext, IEjercicioDBContext
    {
        public EjercicioDBContext(DbContextOptions<EjercicioDBContext> options)
            : base(options)
        {
        }
        public DbSet<Persona> Personas { get; set; }
        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
