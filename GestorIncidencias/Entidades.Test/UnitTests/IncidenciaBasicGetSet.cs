using NUnit.Framework;
using System;
using System.Text;

namespace Entidades.Test.UnitTest
{
    public class IncidenciaBasicGetSet
    {
        private const int TestInt = 100;
        private const int TestInt2 = 0x6F;
        private const string TestString = "aaa-bbb_ccc!ddd~eee;fff/gggñfffégggøhhh";
        private const string TestString2 = ";--";

        public Incidencia Incidencia { get; set; }

        [SetUp]
        public void Setup()
        {
            Incidencia = new Incidencia();
        }

        [Test]
        public void GetSetId()
        {
            Incidencia.IdIncidencia = TestInt;
            Assert.That(Incidencia.IdIncidencia, Is.EqualTo(TestInt));
            Assert.That(Incidencia.IdIncidencia, Is.EqualTo(Incidencia.IdIncidencia));
            Assert.That(Incidencia.IdIncidencia, Is.EqualTo(TestInt));
            Incidencia.IdIncidencia = TestInt2;
            Assert.That(Incidencia.IdIncidencia, Is.Not.EqualTo(TestInt));
            Assert.That(Incidencia.IdIncidencia, Is.EqualTo(TestInt2));
        }

        [Test]
        public void GetSetTimestamp()
        {
            var now = DateTime.Now;
            var before = now.AddDays(-1);
            Incidencia.Timestamp = now;
            Assert.That(Incidencia.Timestamp, Is.EqualTo(now));
            Assert.That(Incidencia.Timestamp, Is.EqualTo(Incidencia.Timestamp));
            Assert.That(Incidencia.Timestamp, Is.EqualTo(now));
            Incidencia.Timestamp = before;
            Assert.That(Incidencia.Timestamp, Is.Not.EqualTo(now));
            Assert.That(Incidencia.Timestamp, Is.EqualTo(before));
        }

        [Test]
        public void GetSetAsunto()
        {
            Incidencia.Asunto = TestString;
            Assert.That(Incidencia.Asunto, Is.EqualTo(TestString));
            Assert.That(Incidencia.Asunto, Is.EqualTo(Incidencia.Asunto));
            Assert.That(Incidencia.Asunto, Is.EqualTo(TestString));
            Incidencia.Asunto = TestString2;
            Assert.That(Incidencia.Asunto, Is.Not.EqualTo(TestString));
            Assert.That(Incidencia.Asunto, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetComentario()
        {
            Incidencia.Comentario = TestString;
            Assert.That(Incidencia.Comentario, Is.EqualTo(TestString));
            Assert.That(Incidencia.Comentario, Is.EqualTo(Incidencia.Comentario));
            Assert.That(Incidencia.Comentario, Is.EqualTo(TestString));
            Incidencia.Comentario = TestString2;
            Assert.That(Incidencia.Comentario, Is.Not.EqualTo(TestString));
            Assert.That(Incidencia.Comentario, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetAula()
        {
            Incidencia.Aula = TestString;
            Assert.That(Incidencia.Aula, Is.EqualTo(TestString));
            Assert.That(Incidencia.Aula, Is.EqualTo(Incidencia.Aula));
            Assert.That(Incidencia.Aula, Is.EqualTo(TestString));
            Incidencia.Aula = TestString2;
            Assert.That(Incidencia.Aula, Is.Not.EqualTo(TestString));
            Assert.That(Incidencia.Aula, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetEquipo()
        {
            Incidencia.Equipo = TestString;
            Assert.That(Incidencia.Equipo, Is.EqualTo(TestString));
            Assert.That(Incidencia.Equipo, Is.EqualTo(Incidencia.Equipo));
            Assert.That(Incidencia.Equipo, Is.EqualTo(TestString));
            Incidencia.Equipo = TestString2;
            Assert.That(Incidencia.Equipo, Is.Not.EqualTo(TestString));
            Assert.That(Incidencia.Equipo, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetCerrada()
        {
            Assert.That(Incidencia.Cerrada, Is.False);
            Incidencia.Cerrada = true;
            Assert.That(Incidencia.Cerrada);
            Assert.That(Incidencia.Cerrada, Is.EqualTo(Incidencia.Cerrada));
            Assert.That(Incidencia.Cerrada);
            Incidencia.Cerrada = false;
            Assert.That(Incidencia.Cerrada, Is.False);
            Assert.That(Incidencia.Cerrada, Is.EqualTo(Incidencia.Cerrada));
        }

        [Test]
        public void GetSetCentroId()
        {
            Incidencia.CentroId = TestString;
            Assert.That(Incidencia.CentroId, Is.EqualTo(TestString));
            Assert.That(Incidencia.CentroId, Is.EqualTo(Incidencia.CentroId));
            Assert.That(Incidencia.CentroId, Is.EqualTo(TestString));
            Incidencia.CentroId = TestString2;
            Assert.That(Incidencia.CentroId, Is.Not.EqualTo(TestString));
            Assert.That(Incidencia.CentroId, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetCentro()
        {
            Incidencia.Centro = new Centro { IdCentro = "TestCentro" };
            Assert.That(Incidencia.Centro, Is.Not.Null);
            Assert.That(Incidencia.Centro.IdCentro, Is.EqualTo("TestCentro"));
            Incidencia.Centro = null;
            Assert.That(Incidencia.Centro, Is.Null);
        }
    }
}