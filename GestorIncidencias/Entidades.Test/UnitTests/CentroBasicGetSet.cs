using NUnit.Framework;
using System.Text;

namespace Entidades.Test.UnitTest
{
    public class CentroBasicGetSet
    {
        private const string TestString = "aaa-bbb_ccc!ddd~eee;fff/gggñfffégggøhhh";
        private const string TestString2 = ";--";
        private byte[] TestStringBytes { get { return Encoding.UTF8.GetBytes(TestString); } }
        private byte[] TestString2Bytes { get { return Encoding.UTF8.GetBytes(TestString2); } }

        public Centro Centro { get; set; }

        [SetUp]
        public void Setup()
        {
            Centro = new Centro();
        }

        [Test]
        public void GetSetId()
        {
            Centro.IdCentro = TestString;
            Assert.That(Centro.IdCentro, Is.EqualTo(TestString));
            Assert.That(Centro.IdCentro, Is.EqualTo(Centro.IdCentro));
            Assert.That(Centro.IdCentro, Is.EqualTo(TestString));
            Centro.IdCentro = TestString2;
            Assert.That(Centro.IdCentro, Is.Not.EqualTo(TestString));
            Assert.That(Centro.IdCentro, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetAulas()
        {
            Centro.Aulas = TestString;
            Assert.That(Centro.Aulas, Is.EqualTo(TestString));
            Assert.That(Centro.Aulas, Is.EqualTo(Centro.Aulas));
            Assert.That(Centro.Aulas, Is.EqualTo(TestString));
            Centro.Aulas = TestString2;
            Assert.That(Centro.Aulas, Is.Not.EqualTo(TestString));
            Assert.That(Centro.Aulas, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetClaveUsuario()
        {
            Centro.ClaveUsuario = TestString;
            Assert.That(Centro.ClaveUsuario, Is.EqualTo(TestString));
            Assert.That(Centro.ClaveUsuario, Is.EqualTo(Centro.ClaveUsuario));
            Assert.That(Centro.ClaveUsuario, Is.EqualTo(TestString));
            Centro.ClaveUsuario = TestString2;
            Assert.That(Centro.ClaveUsuario, Is.Not.EqualTo(TestString));
            Assert.That(Centro.ClaveUsuario, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetClaveAdmin()
        {
            Centro.ClaveAdmin = TestString;
            Assert.That(Centro.ClaveAdmin, Is.EqualTo(TestString));
            Assert.That(Centro.ClaveAdmin, Is.EqualTo(Centro.ClaveAdmin));
            Assert.That(Centro.ClaveAdmin, Is.EqualTo(TestString));
            Centro.ClaveAdmin = TestString2;
            Assert.That(Centro.ClaveAdmin, Is.Not.EqualTo(TestString));
            Assert.That(Centro.ClaveAdmin, Is.EqualTo(TestString2));
        }

        [Test]
        public void GetSetSaltUsuario()
        {
            Centro.SaltUsuario = TestStringBytes;
            Assert.That(Centro.SaltUsuario, Is.EqualTo(TestStringBytes));
            Assert.That(Centro.SaltUsuario, Is.EqualTo(Centro.SaltUsuario));
            Assert.That(Centro.SaltUsuario, Is.EqualTo(TestStringBytes));
            Centro.SaltUsuario = TestString2Bytes;
            Assert.That(Centro.SaltUsuario, Is.Not.EqualTo(TestStringBytes));
            Assert.That(Centro.SaltUsuario, Is.EqualTo(TestString2Bytes));
        }

        [Test]
        public void GetSetSaltAdmin()
        {
            Centro.SaltAdmin = TestStringBytes;
            Assert.That(Centro.SaltAdmin, Is.EqualTo(TestStringBytes));
            Assert.That(Centro.SaltAdmin, Is.EqualTo(Centro.SaltAdmin));
            Assert.That(Centro.SaltAdmin, Is.EqualTo(TestStringBytes));
            Centro.SaltAdmin = TestString2Bytes;
            Assert.That(Centro.SaltAdmin, Is.Not.EqualTo(TestStringBytes));
            Assert.That(Centro.SaltAdmin, Is.EqualTo(TestString2Bytes));
        }
    }
}