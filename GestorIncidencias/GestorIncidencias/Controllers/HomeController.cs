using GestorIncidencias.Models;
using System.Linq;
using System.Web.Mvc;
using GestorIncidencias.Models.Binding;
using System.Security.Claims;
using GestorIncidencias.Helpers;
using System.Security.Principal;

namespace GestorIncidencias.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextoIncidencias contexto = null;

        public HomeController(ContextoIncidencias contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.MensajeError = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginQuery query)
        {
            var centro = contexto.Centros.FirstOrDefault(c => c.IdCentro == query.Centro);

            if (centro == null)
            {
                ViewBag.MensajeError = "Centro inválido.";
                return View();
            }

            string action = null;
            string role = null;

            if (CryptoTools.ValidateHash(query.Clave, centro.SaltUsuario, centro.ClaveUsuario))
            {
                action = "Create";
                role = Roles.User;
            }
            else if (CryptoTools.ValidateHash(query.Clave, centro.SaltAdmin, centro.ClaveAdmin))
            {
                action = "Incidencias";
                role = Roles.Admin;
            }

            if (action != null)
            {
                var identity = NewUserIdentity(User.Identity);
                identity.AddClaim(new Claim("centro", centro.IdCentro));
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                return RedirectToAction(action);
            }else{
                ViewBag.MensajeError = "Contraseña incorrecta.";
                return View();
            }
        }


        public ActionResult Incidencias()
        {
            //Comprobacion de nulos
            var centro = GetUserCentro();
            var role = GetUserRole();
            if (centro == null || role == null || role.Value != Roles.Admin)
            {
                return RedirectToAction("Index");
            }

            //Comparar String puede ser inseguro
            ViewBag.ListaIncidencias = contexto.Incidencias.Where(incidencia => (!incidencia.Cerrada) && (incidencia.Centro.IdCentro == centro.Value)).ToList();

            return View();
        }

        public ActionResult Create()
        {
            var centro = GetUserCentro();
            if (centro == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Your application description page.";
            ViewBag.ListaAulas = contexto.Centros.FirstOrDefault(c => c.IdCentro == centro.Value)?.Aulas?.ToArray() ?? new string[] { };
            return View();
        }

        private Claim GetUserCentro()
        {
            return ((ClaimsIdentity)User.Identity).FindFirst("centro");
        }

        private Claim GetUserRole()
        {
            return ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Role);
        }

        //Crea o renueva la identificacion del usuario al logearse
        private ClaimsIdentity NewUserIdentity(IIdentity identity)
        {
            var newIdentity = (ClaimsIdentity)identity;
            var previousCentro = GetUserCentro();
            var previousRole = GetUserRole();
            if (previousCentro != null)
            {
                newIdentity.RemoveClaim(previousCentro);
            }

            if (previousRole != null)
            {
                newIdentity.RemoveClaim(previousRole);
            }

            return newIdentity;
        }

        private static class Roles
        {
            public const string User = "u";
            public const string Admin = "a";
        }
    }
}