using System;
using System.Collections.Generic;
using System.Linq;
using Effort;
using Entidades;
using GestorIncidencias.Helpers;

namespace GestorIncidencias.Models
{
    public class ContextoTransistente : ContextoIncidencias
    {
        public ContextoTransistente() : base(DbConnectionFactory.CreatePersistent("ContextoTransistente"))
        {
            if (Centros.Count() == 0)
            {
                Seed();
            }
        }

        public void Seed()
        {

            var centros = Centros.AddRange(new[]
            {
                new Centro() { IdCentro = "CentroA", ListaAulas = new [] { "1-1", "1-2", "2-1" }, ClaveUsuario = "1234", ClaveAdmin = "admin1234" },
                new Centro() { IdCentro = "CentroB", ListaAulas = new [] { "a", "b", "c" }, ClaveUsuario = "aaa", ClaveAdmin = "adminaaa" },
                new Centro() { IdCentro = "CentroC", ListaAulas = new [] { "A1-42" } , ClaveUsuario = "bbb", ClaveAdmin = "adminbbb" },
            }).ToArray();

            foreach (Centro centro in centros)
            {
                centro.SaltUsuario = CryptoTools.GenerateSalt();
                centro.ClaveUsuario = CryptoTools.GenerateHash(centro.ClaveUsuario, centro.SaltUsuario);
                centro.SaltAdmin = CryptoTools.GenerateSalt();
                centro.ClaveAdmin = CryptoTools.GenerateHash(centro.ClaveAdmin, centro.SaltAdmin);
            }

            Incidencias.AddRange(new[]
            {
                new Incidencia() { Timestamp = DateTime.UtcNow, Asunto = "Incidencia 1", Comentario = "Comentario 1", Centro = centros[1], Aula = "b", Equipo = "E11", Cerrada = true },
                new Incidencia() { Timestamp = DateTime.UtcNow, Asunto = "Incidencia 2", Comentario = "Comentario 2", Centro = centros[0], Aula = "1-1", Equipo = "E26", Cerrada = true },
                new Incidencia() { Timestamp = DateTime.UtcNow, Asunto = "Incidencia 3", Comentario = "Comentario 3", Centro = centros[0], Aula = "2-1", Equipo = "E27", Cerrada = false },
                new Incidencia() { Timestamp = DateTime.UtcNow, Asunto = "Incidencia 4", Comentario = "Comentario 4", Centro = centros[1], Aula = "c", Equipo = "E32", Cerrada = false },
                new Incidencia() { Timestamp = DateTime.UtcNow, Asunto = "Incidencia 5", Comentario = "Comentario 5", Centro = centros[2], Aula = "A1-42", Equipo = "E49", Cerrada = false },
                new Incidencia() { Timestamp = DateTime.UtcNow, Asunto = "Incidencia 6", Comentario = "Comentario 6", Centro = centros[2], Aula = "A1-42", Equipo = "E50", Cerrada = false }
            });

            SaveChanges();
        }

    }
}