using Ejercicio.Entities;
using Ejercicio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejercicio.Repositories
{
    public class PersonasRepository : IPersonasRepository
    {
        private IEjercicioDBContext _dbcontext;
        public PersonasRepository(IEjercicioDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<int> Create(Persona personas)
        {
            _dbcontext.Personas.Add(personas);
            await _dbcontext.SaveChanges();
            return personas.Id;
        }
        public async Task<List<Persona>> GetAll()
        {
            var personas = await _dbcontext.Personas.ToListAsync<Persona>();
            return personas;
        }
        public async Task<Persona> GetById(int id)
        {
            var personas = await _dbcontext.Personas.Where(empid => empid.Id == id).FirstOrDefaultAsync();
            return personas;
        }
        public async Task<Persona> Randome()
        {
            var personas = await _dbcontext.Personas.OrderBy(r => Guid.NewGuid()).FirstOrDefaultAsync();
            return personas;
        }
        public async Task<string> Update(int id, Persona persona)
        {
            var personaupt = await _dbcontext.Personas.Where(personaid => personaid.Id == id).FirstOrDefaultAsync();
            if (personaupt == null) return "La persona no existe";
            personaupt.Nombre = persona.Nombre;
            personaupt.Apellido = persona.Apellido;
            personaupt.FechaNacimiento = persona.FechaNacimiento;
            personaupt.Vigente = persona.Vigente;
            await _dbcontext.SaveChanges();
            return "La información de la persona fue modificada";
        }
        public async Task<string> Delete(int id)
        {
            var personadel = _dbcontext.Personas.Where(personaid => personaid.Id == id).FirstOrDefault();
            if (personadel == null) return "La persona no existe";
            _dbcontext.Personas.Remove(personadel);
            await _dbcontext.SaveChanges();
            return "La persona fue eliminada";
        }
    }
}
