using Entidades;
using GestorIncidencias.Models;
using NMemory.Exceptions;
using NUnit.Framework;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace GestorIncidencias.Test.UnitTest
{
    public class ContextoTransistenteTests
    {

        public ContextoTransistente Contexto { get; set; }

        [SetUp]
        public void Setup()
        {
            Contexto = new ContextoTransistente();
        }

        [Test]
        public void HasCentros()
        {
            Assert.That(Contexto.Centros.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void CanCreateCentros()
        {
            var initialAmount = Contexto.Centros.Count();
            Contexto.Centros.Add(new Centro { IdCentro = "TestCreate" });
            Contexto.SaveChanges();
            Assert.That(Contexto.Centros.Count(), Is.EqualTo(initialAmount + 1));
        }

        [Test]
        public void CanDeleteCentros()
        {
            var initialAmount = Contexto.Centros.Count();
            var centro = Contexto.Centros.Add(new Centro { IdCentro = "TestDelete" });
            Contexto.SaveChanges();
            Contexto.Centros.Remove(centro);
            Contexto.SaveChanges();
            Assert.That(Contexto.Centros.Count(), Is.EqualTo(initialAmount));
        }

        [Test]
        public void HasIncidencias()
        {
            Assert.That(Contexto.Incidencias.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void CanCreateIncidencias()
        {
            var initialAmount = Contexto.Incidencias.Count();
            Contexto.Incidencias.Add(new Incidencia());
            Contexto.SaveChanges();
            Assert.That(Contexto.Incidencias.Count(), Is.EqualTo(initialAmount + 1));
        }

        [Test]
        public void CanDeleteIncidencias()
        {
            var initialAmount = Contexto.Incidencias.Count();
            var incidencia = Contexto.Incidencias.Add(new Incidencia());
            Contexto.SaveChanges();
            Contexto.Incidencias.Remove(incidencia);
            Contexto.SaveChanges();
            Assert.That(Contexto.Incidencias.Count(), Is.EqualTo(initialAmount));
        }

        [Test]
        public void InvalidCentroFK()
        {
            var incidencia = Contexto.Incidencias.Add(new Incidencia() { CentroId = "TestError" });
            Assert.That(() => Contexto.SaveChanges(), Throws.InstanceOf(typeof(DbUpdateException)).And.InnerException.InnerException.InnerException.InstanceOf(typeof(ForeignKeyViolationException)));
        }

        [Test]
        public void ValidCentroFK()
        {
            Contexto.Centros.Add(new Centro { IdCentro = "TestFK" });
            Contexto.Incidencias.Add(new Incidencia() { CentroId = "TestFK" });
            Assert.That(() => Contexto.SaveChanges(), Throws.Nothing);
            Incidencia incidencia = null;
            Assert.That(() => incidencia = Contexto.Incidencias.Include("Centro").First(i => i.CentroId == "TestFK"), Throws.Nothing);
            Assert.That(incidencia, Is.Not.Null);
            Assert.That(incidencia.Centro, Is.Not.Null);
            Assert.That(incidencia.Centro.IdCentro == "TestFK");
        }
    }
}