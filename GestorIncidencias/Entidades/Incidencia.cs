using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Incidencia
    {
        [Key]
        public int IdIncidencia { get; set; }
        public DateTime Timestamp { get; set; }
        public string Asunto { get; set; }
        public string Comentario { get; set; }
        public string Aula { get; set; }
        public string Equipo { get; set; }
        public bool Cerrada { get; set; }

        [ForeignKey("Centro")]
        public string CentroId { get; set; }
        public virtual Centro Centro { get; set; }
    }
}
