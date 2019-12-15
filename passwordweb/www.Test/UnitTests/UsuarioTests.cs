using NUnit.Framework;
using www.Models;

namespace www.Test.UnitTests
{
    public class UsuarioTests
    {
        private const string Cuenta = "TestCuenta";
        private const string Nombre = "Alfredo";
        private const string Apellidos = "Escual";
        private const string Email = "Alfredo@escual.es";
        private const string Password = "testclave1234";
        private const int Rol = 0;
        private const string DefaultPassword = "P@ssw0rd";

        public Usuario Usuario { get; set; }

        [SetUp]
        public void Setup()
        {
            Usuario = new Usuario(Cuenta, Nombre, Apellidos, Email, Rol);
        }

        /*Requisito 4*/
        [Test]
        public void CanCreateUser()
        {
            Usuario UsuarioNuevo = new Usuario();
            Assert.IsInstanceOf<Usuario>(UsuarioNuevo);
        }

        [Test]
        public void CanCreateUserWithCuenta()
        {
            Usuario UsuarioNuevo = new Usuario(Cuenta);
            Assert.IsInstanceOf<Usuario>(UsuarioNuevo);
        }

        [Test]
        public void CanCreateFullUser()
        {
            Usuario UsuarioNuevo = new Usuario(Cuenta, Nombre, Apellidos, Email, Rol);
            Assert.IsInstanceOf<Usuario>(UsuarioNuevo);
        }

        [Test]
        public void DoesCambiaPassword()
        {
            string NuevaPassword = "Test123";
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));
            Usuario.CambiaPassword(DefaultPassword, NuevaPassword);
            Assert.IsFalse(Usuario.CompruebaPassword(DefaultPassword));
            Assert.IsTrue(Usuario.CompruebaPassword(NuevaPassword));
        }

        /*Requisito 6*/
        [Test]
        public void InititalPassword()
        {
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));
        }

        [Test]
        public void DoesResetPassword()
        {
            string NuevaPassword = "Test123";
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));
            Usuario.CambiaPassword(DefaultPassword, NuevaPassword);
            Assert.IsFalse(Usuario.CompruebaPassword(DefaultPassword));
            Assert.IsTrue(Usuario.CompruebaPassword(NuevaPassword));
            Usuario.ResetPassword();
            Assert.IsFalse(Usuario.CompruebaPassword(NuevaPassword));
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));
        }

        /*Requisito 8*/
        [Test]
        public void CheckIfIsAdministrador()
        {
            Usuario UsuarioNuevo = new Usuario(Cuenta, Nombre, Apellidos, Email, 0);
            Assert.IsTrue(UsuarioNuevo.EsAdministrador());
            Assert.IsFalse(UsuarioNuevo.EsEvaluador());
            Assert.IsFalse(UsuarioNuevo.EsAspirante());
        }
        [Test]
        public void CheckIfIsEvaluador()
        {
            Usuario UsuarioNuevo = new Usuario(Cuenta, Nombre, Apellidos, Email, 1);
            Assert.IsFalse(UsuarioNuevo.EsAdministrador());
            Assert.IsTrue(UsuarioNuevo.EsEvaluador());
            Assert.IsFalse(UsuarioNuevo.EsAspirante());
        }

        [Test]
        public void CheckIfIsAspirante()
        {
            Usuario UsuarioNuevo = new Usuario(Cuenta, Nombre, Apellidos, Email, 2);
            Assert.IsFalse(UsuarioNuevo.EsAdministrador());
            Assert.IsFalse(UsuarioNuevo.EsEvaluador());
            Assert.IsTrue(UsuarioNuevo.EsAspirante());
        }

        /*Requisito 9*/
        [Test]
        public void PasswordAlphanumeric()
        {
            string passwordInvalidaSoloLetras = "ClaveTest";
            string passwordInvalidaSoloNumeros = "123456";
            string passwordValida = passwordInvalidaSoloLetras + passwordInvalidaSoloNumeros;

            Assert.AreNotEqual(0, Usuario.CambiaPassword(DefaultPassword, passwordInvalidaSoloLetras));
            Assert.IsFalse(Usuario.CompruebaPassword(passwordInvalidaSoloLetras));
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));

            Assert.AreNotEqual(0, Usuario.CambiaPassword(DefaultPassword, passwordInvalidaSoloNumeros));
            Assert.IsFalse(Usuario.CompruebaPassword(passwordInvalidaSoloNumeros));
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));

            Assert.AreEqual(0, Usuario.CambiaPassword(DefaultPassword, passwordValida));
            Assert.IsTrue(Usuario.CompruebaPassword(passwordValida));
            Assert.IsFalse(Usuario.CompruebaPassword(DefaultPassword));
        }

        [Test]
        public void PasswordCase()
        {
            string passwordInvalidaSoloMinusculas = "clavetest123456";
            string passwordInvalidaSoloMayusculas = "CLAVETEST123456";
            string passwordValida = "ClaveTest123456";

            Assert.AreNotEqual(0, Usuario.CambiaPassword(DefaultPassword, passwordInvalidaSoloMinusculas));
            Assert.IsFalse(Usuario.CompruebaPassword(passwordInvalidaSoloMinusculas));
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));

            Assert.AreNotEqual(0, Usuario.CambiaPassword(DefaultPassword, passwordInvalidaSoloMayusculas));
            Assert.IsFalse(Usuario.CompruebaPassword(passwordInvalidaSoloMayusculas));
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));

            Assert.AreEqual(0, Usuario.CambiaPassword(DefaultPassword, passwordValida));
            Assert.IsTrue(Usuario.CompruebaPassword(passwordValida));
            Assert.IsFalse(Usuario.CompruebaPassword(DefaultPassword));
        }

        [Test]
        public void PasswordIsNotName()
        {
            Assert.AreNotEqual(0, Usuario.CambiaPassword(DefaultPassword, Cuenta));
            Assert.IsFalse(Usuario.CompruebaPassword(Cuenta));
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));
        }
    }
}
