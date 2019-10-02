using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Incidencia
    {
        public int IdIncidencia { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Asunto { get; set; }
        public string Comentario { get; set; }
        public Centro Centro { get; set; }
        public string Aula { get; set; }
        public string Equipo { get; set; }
        public bool Cerrada { get; set; }
    }
}
