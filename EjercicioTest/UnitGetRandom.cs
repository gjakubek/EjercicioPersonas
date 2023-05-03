using Ejercicio.Controllers;
using Ejercicio.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
namespace EjercicioTest
{
    public class UnitGetRandom
    {
        private PersonasController _personasController;

        [SetUp]
        public void Setup()
        {
            var mockPersonas = new Mock<IPersonasRepository>();
            _personasController = new PersonasController(mockPersonas.Object);
        }
        [Test]
        public void TestGetRandom()
        {
            var resultado = _personasController.GetRandom();
            var okResultado = resultado.Result as OkObjectResult;
            Assert.IsNotNull(okResultado);
            Assert.AreEqual(200, okResultado.StatusCode);
        }
    }
}
