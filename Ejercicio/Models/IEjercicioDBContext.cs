using Ejercicio.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ejercicio.Models
{
    public interface IEjercicioDBContext
    {
        DbSet<Persona> Personas { get; set; }
        Task<int> SaveChanges();
    }
}
