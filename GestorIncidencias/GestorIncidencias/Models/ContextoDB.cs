using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;

namespace GestorIncidencias.Models
{
    public class ContextoDB: IContextoIncidencias
    {
        public IQueryable<Incidencia> Indicencias { get; set; } = new[]{
                new Incidencia() { Asunto = "a" },
                new Incidencia() { Asunto = "b" },
                new Incidencia() { Asunto = "c" },
            }.AsQueryable();

        public IQueryable<Centro> Centros { get; set; } = new[]
        {
            new Centro(),
            new Centro(),
            new Centro(),
        }.AsQueryable();

    }
}