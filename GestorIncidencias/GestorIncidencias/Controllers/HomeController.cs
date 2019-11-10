using GestorIncidencias.Models;
using System.Linq;
using System.Web.Mvc;
using GestorIncidencias.Models.Binding;
using GestorIncidencias.Helpers;
using Entidades;
using System;

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
            if (IsLoggedIn())
            {
                return RedirectToAction(IsAdmin() ? "Incidencias" : "Create");
            }
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
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            ViewBag.ListaIncidencias = contexto.Incidencias.Where(incidencia => (!incidencia.Cerrada) && (incidencia.Centro.IdCentro == SesionUsuario.Centro.IdCentro)).ToList();

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index");
            }
            ViewBag.ListaAulas = new SelectList(SesionUsuario.Centro.ListaAulas ?? new string[] { }); ;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateQuery query)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index");
            }

            var centro = SesionUsuario.Centro;

            if (string.IsNullOrEmpty(query.Aula) || centro.ListaAulas.Length == 0 || !centro.ListaAulas.Contains(query.Aula))
            {
                ViewBag.MensajeError = "Aula no válida.";
            }
            else if (string.IsNullOrWhiteSpace(query.Asunto))
            {
                ViewBag.MensajeError = "Asunto no puede estar vacio.";
            }
            else
            {
                Incidencia incidencia = new Incidencia()
                {
                    Timestamp = DateTime.UtcNow,
                    Asunto = query.Asunto,
                    Comentario = query.Comentario,
                    Aula = query.Aula,
                    Equipo = query.Equipo,
                    Cerrada = false,
                    CentroId = centro.IdCentro
                };
                contexto.Incidencias.Add(incidencia);
                contexto.SaveChanges();
                ViewBag.MensajeOk = "La incidendia ha sido enviada correctamente.";
            }

            return Create();
        }

        [HttpPost]
        public ActionResult Cerrar(int idIncidencia)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            var incidencia = contexto.Incidencias.FirstOrDefault(i => i.IdIncidencia == idIncidencia);
            if (incidencia != null)
            {
                incidencia.Cerrada = true;
                contexto.SaveChanges();
            }
            return RedirectToAction("Incidencias");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            SesionUsuario = null;
            return RedirectToAction("Index");
        }

        public bool IsLoggedIn()
        {
            return SesionUsuario?.Centro != null;
        }

        public bool IsAdmin()
        {
            return IsLoggedIn() && SesionUsuario.EsAdmin;
        }
    }
}