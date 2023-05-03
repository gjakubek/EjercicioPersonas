using Ejercicio.Controllers;
using Ejercicio.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
namespace EjercicioTest
{
    public class UnitDelete
    {
        private PersonasController _personasController;

        [SetUp]
        public void Setup()
        {
            var mockPersonas = new Mock<IPersonasRepository>();
            _personasController = new PersonasController(mockPersonas.Object);
        }

        [Test]
        public void TestDelete()
        {
            var resultado = _personasController.Delete(100).Result;
            var okResultado = resultado as OkObjectResult;
            Assert.IsNotNull(okResultado);
            Assert.AreEqual(200, okResultado.StatusCode);
        }
    }
}
