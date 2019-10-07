using GestorIncidencias.Models;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestorIncidencias.Models.Binding;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace GestorIncidencias.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContextoIncidencias contexto = null;

        public HomeController(IContextoIncidencias contexto)
        {
            this.contexto = contexto;
        }

        public ActionResult Index()
        {
            ViewBag.ListaIncidencias = contexto.Incidencias.Where(incidencia => !incidencia.Cerrada).ToList();
            return View();
        }

        public ActionResult About()
        {
            var centro = GetUserCentro();
            if (centro == null)
            {
                return RedirectToAction("Contact");
            }
            ViewBag.Message = "Your application description page.";
            ViewBag.ListaAulas = contexto.Centros.FirstOrDefault(c => c.IdCentro == centro.Value)?.Aulas?.ToArray() ?? new string[] { };
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(LoginQuery query)
        {
            var centro = contexto.Centros.FirstOrDefault(c => c.IdCentro == query.Centro);

            if (centro == null)
            {
                return View();
            }

            if (centro.ClaveUsuario == query.Clave)
            {
                var identity = (ClaimsIdentity)User.Identity;

                var previous = GetUserCentro();
                if (previous != null)
                {
                    identity.RemoveClaim(previous);
                }

                identity.AddClaim(new Claim("centro", centro.IdCentro));

                return RedirectToAction("About");
            }

            return View();

        }

        private Claim GetUserCentro()
        {
            return ((ClaimsIdentity)User.Identity).FindFirst("centro");
        }
    }
}