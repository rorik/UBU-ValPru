using GestorIncidencias.Models.Binding;
using NUnit.Framework;

namespace GestorIncidencias.Test.UnitTest
{
    public class LoginQueryTests
    {

        private const string TestString = "aaa-bbb_ccc!ddd~eee;fff/gggñfffégggøhhh";

        public LoginQuery LoginQuery { get; set; }

        [SetUp]
        public void Setup()
        {
            LoginQuery = new LoginQuery();
        }

        [Test]
        public void GetSetCentro()
        {
            Assert.That(LoginQuery.Centro, Is.Null);
            LoginQuery.Centro = TestString;
            Assert.That(LoginQuery.Centro, Is.EqualTo(TestString));
            Assert.That(LoginQuery.Clave, Is.Not.EqualTo(TestString));
        }

        [Test]
        public void GetSetClave()
        {
            Assert.That(LoginQuery.Clave, Is.Null);
            LoginQuery.Clave = TestString;
            Assert.That(LoginQuery.Clave, Is.EqualTo(TestString));
            Assert.That(LoginQuery.Centro, Is.Not.EqualTo(TestString));
        }
    }
}