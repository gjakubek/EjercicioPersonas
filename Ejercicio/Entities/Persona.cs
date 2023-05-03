using System;

namespace Ejercicio.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Vigente { get; set; }
    }
}
