using Ejercicio.Controllers;
using Ejercicio.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
namespace EjercicioTest
{
    public class UnitGetById
    {
        private PersonasController _personasController;

        [SetUp]
        public void Setup()
        {
            var mockPersonas = new Mock<IPersonasRepository>();
            _personasController = new PersonasController(mockPersonas.Object);
        }
        [Test]
        public void TestGetById()
        {
            var resultado = _personasController.GetById(1).Result;
            var okResultado = resultado as OkObjectResult;
            Assert.IsNotNull(okResultado);
            Assert.AreEqual(200, okResultado.StatusCode);
        }
    }
}
