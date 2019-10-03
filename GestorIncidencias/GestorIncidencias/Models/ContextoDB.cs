using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;

namespace GestorIncidencias.Models
{
    public class ContextoDB: IContextoIncidencias
    {
        TimeSpan epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);

        public IQueryable<Centro> Centros { get; set; } = new[]
        {
            new Centro() { IdCentro = "CentroA", },
            new Centro() { IdCentro = "CentroB" },
            new Centro() { IdCentro = "CentroC" },
        }.AsQueryable();

        public IQueryable<Incidencia> Incidencias { get; set; } = new[]{
                new Incidencia() { IdIncidencia = 1, Timestamp = DateTimeOffset.UtcNow, Asunto = "Incidencia 1", Comentario = "Comentario 1", Centro = null, Aula = "Aula1", Equipo = "E11", Cerrada = true },
                new Incidencia() { IdIncidencia = 2, Timestamp = DateTimeOffset.UtcNow, Asunto = "Incidencia 2", Comentario = "Comentario 2", Centro = null, Aula = "Aula2", Equipo = "E26", Cerrada = true },
                new Incidencia() { IdIncidencia = 3, Timestamp = DateTimeOffset.UtcNow, Asunto = "Incidencia 3", Comentario = "Comentario 3", Centro = null, Aula = "Aula3", Equipo = "E32", Cerrada = false },
                new Incidencia() { IdIncidencia = 4, Timestamp = DateTimeOffset.UtcNow, Asunto = "Incidencia 4", Comentario = "Comentario 4", Centro = null, Aula = "Aula4", Equipo = "E49", Cerrada = false },
                new Incidencia() { IdIncidencia = 5, Timestamp = DateTimeOffset.UtcNow, Asunto = "Incidencia 5", Comentario = "Comentario 5", Centro = null, Aula = "Aula5", Equipo = "E50", Cerrada = false }
            }.AsQueryable();

    }
}