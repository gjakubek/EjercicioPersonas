using Ejercicio.Controllers;
using Ejercicio.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EjercicioTest
{
    public class UnitGetAll
    {
        private PersonasController _personasController;

        [SetUp]
        public void Setup()
        {
            var mockPersonas = new Mock<IPersonasRepository>();
            _personasController = new PersonasController(mockPersonas.Object);
        }

        [Test]
        public void TestGetAll()
        {
            var resultado = _personasController.GetAll().Result;
            var okResultado = resultado as OkObjectResult;
            Assert.IsNotNull(okResultado);
            Assert.AreEqual(200, okResultado.StatusCode);
        }
    }
}