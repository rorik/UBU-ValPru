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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.ListaIncidenciasId = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.IdIncidencia).ToList();

            ViewBag.ListaIncidenciasTimestamp = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.Timestamp).ToList();

            ViewBag.ListaIncidenciasAsunto = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.Asunto).ToList();

            ViewBag.ListaIncidenciasComentario = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.Comentario).ToList();

            ViewBag.ListaIncidenciasCentro = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.Centro).ToList();

            ViewBag.ListaIncidenciasAula = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.Aula).ToList();

            ViewBag.ListaIncidenciasEquipo = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.Equipo).ToList();

            ViewBag.ListaIncidenciasCerrada = contexto.Incidencias
                                                    .Where(incidencia => incidencia.Cerrada == false)
                                                    .Select(incidencia => incidencia.Cerrada).ToList();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}