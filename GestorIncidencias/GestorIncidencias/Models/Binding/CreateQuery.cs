using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestorIncidencias.Models.Binding
{
    public class CreateQuery
    {
        public string Aula { get; set; }
        public string Asunto { get; set; }
        public string Comentario { get; set; }
        public string Equipo { get; set; }
    }
}