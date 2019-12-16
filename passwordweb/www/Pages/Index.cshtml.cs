using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using www.Models;

namespace www.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public static List<Usuario> Usuarios { get; set; } = new List<Usuario>
        {
            new Usuario("usuario1", "abc", "def ghi", "a@example.com", 0),
            new Usuario("usuario2", "jkl", "mno pqr", "b@example.com", 1),
            new Usuario("usuario3", "stu", "vwx yzz", "c@example.com", 2),
            new Usuario("TestCuenta", "zzz", "zzz zzz", "d@example.com", 0),
        };
        
        public IndexModel()
        {
            Usuarios[3].ResetPassword();
            Usuarios[3].CambiaPassword("P@ssw0rd", "testClave1234");
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            Username = Request.Form[nameof(Username)];
            Password = Request.Form[nameof(Password)];
            NewPassword = Request.Form[nameof(NewPassword)];
            ConfirmNewPassword = Request.Form[nameof(ConfirmNewPassword)];

            Message = null;

            if (string.IsNullOrEmpty(Username))
            {
                Message = "El campo de usuario no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                Message = "El campo de contraseña no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(NewPassword))
            {
                Message = "El campo de nueva contraseña no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(ConfirmNewPassword))
            {
                Message = "El campo de repetir nueva contraseña no puede estar vacío.";
            }
            else if (NewPassword != ConfirmNewPassword)
            {
                Message = "Las contraseñas no coinciden.";
            }
            else if (!Usuario.RequisitosPW(NewPassword, Username))
            {
                Message = "La nueva contraseña no cumple con los requisitos.";
            }
            else
            {
                var user = Usuarios.FirstOrDefault(u => u.Cuenta == Username);
                if (user == null)
                {
                    Message = "El usuario no existe.";
                }
                else
                {
                    var changed = user.CambiaPassword(Password, NewPassword) == 0;
                    if (changed)
                    {
                        Message = "La contraseña ha sido cambiada.";
                    }
                    else
                    {
                        Message = "La contraseña no es válida.";
                    }
                }
            }

        }
    }
}
