﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Incidencia
    {
        [Key]
        public int IdIncidencia { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Asunto { get; set; }
        public string Comentario { get; set; }
        public string Aula { get; set; }
        public string Equipo { get; set; }
        public bool Cerrada { get; set; }
        public virtual Centro Centro { get; set; }
    }
}
