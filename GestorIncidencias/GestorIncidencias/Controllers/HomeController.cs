using GestorIncidencias.Models;
using System.Linq;
using System.Web.Mvc;
using GestorIncidencias.Models.Binding;
using System.Security.Claims;

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

            if (centro.ClaveUsuario == query.Clave)
            {
                var identity = NewUserIdentity((ClaimsIdentity)User.Identity);

                //¿Utilizar un token en la BD para aceptar los campos en la identidad como ya verdaderos?
                identity.AddClaim(new Claim("centro", centro.IdCentro));
                identity.AddClaim(new Claim("clave", centro.ClaveUsuario));

                return RedirectToAction("About");
            }
            else if (centro.ClaveAdmin == query.Clave)
            {
                var identity = NewUserIdentity((ClaimsIdentity)User.Identity);

                //¿Utilizar un token en la BD para aceptar los campos en la identidad como ya verdaderos?
                identity.AddClaim(new Claim("centro", centro.IdCentro));
                identity.AddClaim(new Claim("clave", centro.ClaveAdmin));

                return RedirectToAction("Incidencias");

            }

            return View();

        }

        public ActionResult Incidencias()
        {
            //Comprobacion de nulos
            var centro = GetUserCentro();
            var clave = GetUserClave();
            if (centro == null || clave == null)
            {
                return RedirectToAction("Index");
            }
            //Comprobacion de clave correcta. ¿Podria cambiarse por comprobación de un token?
            var centroCheck = contexto.Centros.FirstOrDefault(c => c.IdCentro == centro.Value);
            if (centroCheck.ClaveAdmin != clave.Value) {
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

        private Claim GetUserClave()
        {
            return ((ClaimsIdentity)User.Identity).FindFirst("clave");
        }

        //Crea o renueva la identificacion del usuario al logearse
        private ClaimsIdentity NewUserIdentity(ClaimsIdentity identity)
        {
            var previousCentro = GetUserCentro();
            var previousClave = GetUserClave();
            if (previousCentro != null)
            {
                identity.RemoveClaim(previousCentro);
            }

            if (previousClave != null)
            {
                identity.RemoveClaim(previousClave);
            }

            return identity;
        }
    }
}