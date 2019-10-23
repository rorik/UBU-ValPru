using GestorIncidencias.Models;
using System.Linq;
using System.Web.Mvc;
using GestorIncidencias.Models.Binding;
using GestorIncidencias.Helpers;

namespace GestorIncidencias.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextoIncidencias contexto = null;

        public SesionUsuario SesionUsuario { get { return Session["usuario"] as SesionUsuario; } set { Session["usuario"] = value; } }

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
            var sesion = new SesionUsuario() { Centro = centro };

            if (CryptoTools.ValidateHash(query.Clave, centro.SaltUsuario, centro.ClaveUsuario))
            {
                action = "Create";
                sesion.EsAdmin = false;
            }
            else if (CryptoTools.ValidateHash(query.Clave, centro.SaltAdmin, centro.ClaveAdmin))
            {
                action = "Incidencias";
                sesion.EsAdmin = true;
            }
            else
            {
                ViewBag.MensajeError = "Contraseña incorrecta.";
                SesionUsuario = null;
                return View();
            }

            SesionUsuario = sesion;
            return RedirectToAction(action);
        }


        public ActionResult Incidencias()
        {
            //Comprobacion de nulos
            var centro = SesionUsuario.Centro;
            if (SesionUsuario?.Centro == null || !SesionUsuario.EsAdmin)
            {
                return RedirectToAction("Index");
            }

            //Comparar String puede ser inseguro
            ViewBag.ListaIncidencias = contexto.Incidencias.Where(incidencia => (!incidencia.Cerrada) && (incidencia.Centro.IdCentro == centro.IdCentro)).ToList();

            return View();
        }

        public ActionResult Create()
        {
            var centro = SesionUsuario?.Centro;
            if (centro == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ListaAulas = centro.Aulas?.ToArray() ?? new string[] { };
            return View();
        }

    }
}