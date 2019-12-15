using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.Models
{
    public class Usuario
    {
        private string Cuenta;
        private string Nombre;
        private string Apellidos;
        private string Email;
        private string Password;
        private int Rol;
        private string DefaultPassword = "P@ssw0rd";

        private string Salt;
        private int RolAdmin = 0;
        private int RolEvaluador = 1;
        private int RolAspirante = 2;

        public Usuario()
        {
            Salt = CryptoTools.GenerateSalt();
            Password = CryptoTools.GenerateHash(DefaultPassword, Salt);
        }

        public Usuario(string _cuenta)
        {
            Cuenta = _cuenta;
            Salt = CryptoTools.GenerateSalt();
            Password = CryptoTools.GenerateHash(DefaultPassword, Salt);
        }

        public Usuario(string _cuenta, string _nombre, string _apellidos, string _eMail, int _rol)
        {
            Cuenta = _cuenta;
            Nombre = _nombre;
            Apellidos = _apellidos;
            Email = _eMail;
            Rol = _rol;
            Salt = CryptoTools.GenerateSalt();
            Password = CryptoTools.GenerateHash(DefaultPassword, Salt);
        }

        public string ResetPassword()
        {
            Password = CryptoTools.GenerateHash(DefaultPassword, Salt);
            return DefaultPassword;
        }

        public int CambiaPassword(string anteriorPassword, string nuevaPassword)
        {
            if (CompruebaPassword(anteriorPassword) && RequisitosPW(nuevaPassword, Cuenta))
            {
                Password = CryptoTools.GenerateHash(nuevaPassword, Salt);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public bool CompruebaPassword(string password)
        {
            return CryptoTools.ValidateHash(password, Password, Salt);
        }

        public int Grabar()
        {
            return 0;
        }

        public int Eliminar()
        {
            return 0;
        }

        public int Restaurar()
        {
            return 0;
        }

        public static bool RequisitosPW(string password, string cuenta)
        {
            //Comprobacion de solo numeros y letras
            if (password.All(char.IsDigit) || password.All(char.IsLetter))
            {
                return false;
            }

            //Comprobacion de mayusculas y minusculas
            if (!(password.Any(char.IsLower) && password.Any(char.IsUpper)))
            {
                return false;
            }

            //Comprobacion de si contraseña es cuenta

            return password != cuenta;
        }

        private bool Validar()
        {
            return true;
        }

        public bool EsAdministrador()
        {
            return Rol == RolAdmin;
        }
        public bool EsEvaluador()
        {
            return Rol == RolEvaluador;
        }
        public bool EsAspirante()
        {
            return Rol == RolAspirante;
        }
    }
}
