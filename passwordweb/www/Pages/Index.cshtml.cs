using Microsoft.AspNetCore.Mvc.RazorPages;
using www.Models;

namespace www.Pages
{
    public class IndexModel : PageModel
    {
        public string Error { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            Username = Request.Form[nameof(Username)];
            Password = Request.Form[nameof(Password)];
            NewPassword = Request.Form[nameof(NewPassword)];
            ConfirmNewPassword = Request.Form[nameof(ConfirmNewPassword)];

            Error = null;

            if (string.IsNullOrEmpty(Username))
            {
                Error = "El campo de usuario no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                Error = "El campo de contraseña no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(NewPassword))
            {
                Error = "El campo de nueva contraseña no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(ConfirmNewPassword))
            {
                Error = "El campo de repetir nueva contraseña no puede estar vacío.";
            }
            else if (NewPassword != ConfirmNewPassword)
            {
                Error = "Las contraseñas no coinciden.";
            }
            else if (!Usuario.RequisitosPW(NewPassword, Username))
            {
                Error = "Las nueva contraseña no cumple con los requisitos.";
            }

        }
    }
}
