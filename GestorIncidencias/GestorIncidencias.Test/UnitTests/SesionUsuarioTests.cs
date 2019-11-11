using Entidades;
using GestorIncidencias.Models;
using NUnit.Framework;

namespace GestorIncidencias.Test.UnitTest
{
    public class SesionUsuarioTests
    {

        public SesionUsuario SesionUsuario { get; set; }

        [SetUp]
        public void Setup()
        {
            SesionUsuario = new SesionUsuario();
        }

        [Test]
        public void GetSetCentro()
        {
            Assert.That(SesionUsuario.Centro, Is.Null);
            SesionUsuario.Centro = new Centro { IdCentro = "TestGetSet" };
            Assert.That(SesionUsuario.Centro, Is.Not.Null);
            Assert.That(SesionUsuario.Centro.IdCentro, Is.EqualTo("TestGetSet"));
        }

        [Test]
        public void GetSetEsAdmin()
        {
            Assert.That(SesionUsuario.EsAdmin, Is.False);
            SesionUsuario.EsAdmin = false;
            Assert.That(SesionUsuario.EsAdmin, Is.False);
            SesionUsuario.EsAdmin = true;
            Assert.That(SesionUsuario.EsAdmin, Is.True);
        }
    }
}