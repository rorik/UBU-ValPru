using GestorIncidencias.Models.Binding;
using NUnit.Framework;

namespace GestorIncidencias.Test.UnitTest
{
    public class CreateQueryTests
    {

        private const string TestString = "aaa-bbb_ccc!ddd~eee;fff/gggñfffégggøhhh";

        public CreateQuery CreateQuery { get; set; }

        [SetUp]
        public void Setup()
        {
            CreateQuery = new CreateQuery();
        }

        [Test]
        public void GetSetAula()
        {
            Assert.That(CreateQuery.Aula, Is.Null);
            CreateQuery.Aula = TestString;
            Assert.That(CreateQuery.Aula, Is.EqualTo(TestString));
        }

        [Test]
        public void GetSetAsunto()
        {
            Assert.That(CreateQuery.Asunto, Is.Null);
            CreateQuery.Asunto = TestString;
            Assert.That(CreateQuery.Asunto, Is.EqualTo(TestString));
        }

        [Test]
        public void GetSetComentario()
        {
            Assert.That(CreateQuery.Comentario, Is.Null);
            CreateQuery.Comentario = TestString;
            Assert.That(CreateQuery.Comentario, Is.EqualTo(TestString));
        }

        [Test]
        public void GetSetEquipo()
        {
            Assert.That(CreateQuery.Equipo, Is.Null);
            CreateQuery.Equipo = TestString;
            Assert.That(CreateQuery.Equipo, Is.EqualTo(TestString));
        }
    }
}