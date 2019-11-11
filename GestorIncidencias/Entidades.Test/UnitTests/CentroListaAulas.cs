using NUnit.Framework;
using System.Linq;

namespace Entidades.Test.UnitTest
{
    public class CentroListaAulas
    {
        private readonly string TestAulas = $"a{Centro.AulasSeparator}b{Centro.AulasSeparator}c{Centro.AulasSeparator}d{Centro.AulasSeparator}e{Centro.AulasSeparator}f{Centro.AulasSeparator}g{Centro.AulasSeparator}h";
        private readonly string TestAulas2 = $"?{Centro.AulasSeparator}{Centro.AulasSeparator}{Centro.AulasSeparator}?";
        private readonly string TestAulas2Simplified = $"?{Centro.AulasSeparator}?";
        private string[] TestAulasList { get { return TestAulas.Split(Centro.AulasSeparator).Where(a => !string.IsNullOrEmpty(a)).ToArray(); ; } }
        private string[] TestAulas2List { get { return TestAulas2.Split(Centro.AulasSeparator).Where(a => !string.IsNullOrEmpty(a)).ToArray(); } }

        public Centro Centro { get; set; }

        [SetUp]
        public void Setup()
        {
            Centro = new Centro();
        }

        [Test]
        public void InititalState()
        {
            Assert.That(Centro.Aulas, Is.Null);
            Assert.That(Centro.ListaAulas, Is.Null);
        }

        [Test]
        public void SetAulas()
        {
            Centro.Aulas = TestAulas;
            Assert.That(Centro.ListaAulas, Is.Not.Null);
            Assert.That(Centro.ListaAulas, Is.EquivalentTo(TestAulasList));
            Assert.That(Centro.ListaAulas, Is.Not.EquivalentTo(TestAulas2));
        }

        [Test]
        public void SetAulas2()
        {
            Centro.Aulas = TestAulas2;
            Assert.That(Centro.ListaAulas, Is.Not.Null);
            Assert.That(Centro.ListaAulas, Is.EquivalentTo(TestAulas2List));
            Assert.That(Centro.ListaAulas, Is.Not.EquivalentTo(TestAulas));
        }

        [Test]
        public void SetListaAulas()
        {
            Centro.ListaAulas = TestAulasList;
            Assert.That(Centro.ListaAulas, Is.Not.Null);
            Assert.That(Centro.ListaAulas, Is.EquivalentTo(TestAulasList));
            Assert.That(Centro.ListaAulas, Is.Not.EquivalentTo(TestAulas2List));
            Assert.That(Centro.Aulas, Is.EqualTo(TestAulas));
            Assert.That(Centro.Aulas, Is.Not.EqualTo(TestAulas2));
        }

        [Test]
        public void SetListaAulas2()
        {
            Centro.ListaAulas = TestAulas2List;
            Assert.That(Centro.ListaAulas, Is.Not.Null);
            Assert.That(Centro.ListaAulas, Is.EquivalentTo(TestAulas2List));
            Assert.That(Centro.ListaAulas, Is.Not.EquivalentTo(TestAulasList));
            Assert.That(Centro.Aulas, Is.Not.EqualTo(TestAulas2));
            Assert.That(Centro.Aulas, Is.EqualTo(TestAulas2Simplified));
            Assert.That(Centro.Aulas, Is.Not.EqualTo(TestAulas));
        }

        [Test]
        public void SetListaAulasNull()
        {
            Centro.ListaAulas = TestAulas2List;
            Assert.That(Centro.ListaAulas, Is.Not.Null);
            Assert.That(Centro.Aulas, Is.Not.Null);
            Centro.ListaAulas = null;
            Assert.That(Centro.ListaAulas, Is.Null);
            Assert.That(Centro.Aulas, Is.Null);
        }
    }
}