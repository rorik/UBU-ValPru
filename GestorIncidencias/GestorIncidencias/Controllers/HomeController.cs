using GestorIncidencias.Models;
using System.Linq;
using System.Web.Mvc;
using GestorIncidencias.Models.Binding;
using System.Security.Claims;
using GestorIncidencias.Helpers;

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
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginQuery query)
        {
            var centro = contexto.Centros.FirstOrDefault(c => c.IdCentro == query.Centro);

            if (centro == null)
            {
                return View();
            }

            if (CryptoTools.ValidateHash(query.Clave, centro.SaltUsuario, centro.ClaveUsuario))
                {
                var identity = NewUserIdentity((ClaimsIdentity)User.Identity);


                identity.AddClaim(new Claim("centro", centro.IdCentro));
                identity.AddClaim(new Claim(ClaimTypes.Role, Roles.User));
                return RedirectToAction("About");
            }
            else if (CryptoTools.ValidateHash(query.Clave, centro.SaltAdmin, centro.ClaveAdmin))
            {
                var identity = NewUserIdentity((ClaimsIdentity)User.Identity);
                
                identity.AddClaim(new Claim("centro", centro.IdCentro));
                identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Admin));

                return RedirectToAction("Incidencias");

            }

            return View();

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

        public ActionResult About()
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
        private ClaimsIdentity NewUserIdentity(ClaimsIdentity identity)
        {
            var previousCentro = GetUserCentro();
            var previousRole = GetUserRole();
            if (previousCentro != null)
            {
                identity.RemoveClaim(previousCentro);
            }

            if (previousRole != null)
            {
                identity.RemoveClaim(previousRole);
            }

            return identity;
        }

        private static class Roles
        {
            public const string User = "u";
            public const string Admin = "a";
        }
    }
}