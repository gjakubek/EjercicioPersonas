using Ejercicio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ejercicio.Repositories
{
    public interface IPersonasRepository
    {
        Task<int> Create(Persona personas);
        Task<List<Persona>> GetAll();
        Task<Persona> GetById(int id);
        Task<Persona> Randome();
        Task<string> Update(int id, Persona personas);
        Task<string> Delete(int id);
    }
}
