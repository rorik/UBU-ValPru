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
            this.Salt = CryptoTools.GenerateSalt();
            this.Password = CryptoTools.GenerateHash(this.DefaultPassword, this.Salt);
        }

        public Usuario(string _cuenta)
        {
            this.Cuenta = _cuenta;
            this.Salt = CryptoTools.GenerateSalt();
            this.Password = CryptoTools.GenerateHash(this.DefaultPassword, this.Salt);
        }

        public Usuario(string _cuenta, string _nombre, string _apellidos, string _eMail, int _rol)
        {
            this.Cuenta = _cuenta;
            this.Nombre = _nombre;
            this.Apellidos = _apellidos;
            this.Email = _eMail;
            this.Rol = _rol;
            this.Salt = CryptoTools.GenerateSalt();
            this.Password = CryptoTools.GenerateHash(this.DefaultPassword, this.Salt);
        }

        public string ResetPassword()
        {
            this.Password = CryptoTools.GenerateHash(this.DefaultPassword, this.Salt);
            return this.DefaultPassword;
        }

        public int CambiaPassword(string anteriorPassword, string nuevaPassword)
        {
            if (this.CompruebaPassword(anteriorPassword) && this.RequisitosPW(nuevaPassword))
            {
                this.Password = CryptoTools.GenerateHash(nuevaPassword, this.Salt);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public bool CompruebaPassword(string password)
        {
            return CryptoTools.ValidateHash(password, this.Password, this.Salt);
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

        private bool RequisitosPW(string password)
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
            if (this.Cuenta != null && password == this.Cuenta)
            {
                return false;
            }

            return true;
        }

        private bool Validar()
        {
            return true;
        }

        public bool EsAdministrador()
        {
            return this.Rol == this.RolAdmin;
        }
        public bool EsEvaluador()
        {
            return this.Rol == this.RolEvaluador;
        }
        public bool EsAspirante()
        {
            return this.Rol == this.RolAspirante;
        }
    }
}
