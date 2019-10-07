using GestorIncidencias.Models;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}