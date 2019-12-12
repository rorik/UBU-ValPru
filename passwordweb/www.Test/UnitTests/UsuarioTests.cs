﻿using NUnit.Framework;
using System.Text;

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
            UsuarioNuevo = new Usuario();
            Assert.IsInstanceOf<Usuario>(UsuarioNuevo);
        }

        [Test]
        public void CanCreateUserWithCuenta()
        {
            UsuarioNuevo = new Usuario(Cuenta);
            Assert.IsInstanceOf<Usuario>(UsuarioNuevo);
        }

        [Test]
        public void CanCreateFullUser()
        {
            UsuarioNuevo = new Usuario(Cuenta, Nombre, Apellidos, Email, Rol);
            Assert.IsInstanceOf<Usuario>(UsuarioNuevo);
        }

        [Test]
        public void DoesCambiaPassword()
        {
            string NuevaPassword = "test123";
            Assert.IsTrue(Usuario.CompruebaPassword(DefaultPassword));
            Usuario.CambiarPassword(DefaultPassword, NuevaPassword);
            Assert.IsFalse(Usuario.CompruebaPassword(DefaultPassword));
            Assert.IsTrue(Usuario.CompruebaPassword(NuevaPassword));
        }
    }
}
