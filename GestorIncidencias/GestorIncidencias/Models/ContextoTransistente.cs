﻿using System;
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
                new Centro() { IdCentro = "CentroA", Aulas = "1-1;1-2;2-1", ClaveUsuario = "1234", ClaveAdmin = "admin1234" },
                new Centro() { IdCentro = "CentroB", Aulas = "a;b;c", ClaveUsuario = "aaa", ClaveAdmin = "adminaaa" },
                new Centro() { IdCentro = "CentroC", Aulas = "0;1;2" , ClaveUsuario = "bbb", ClaveAdmin = "adminbbb" },
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
                new Incidencia() { IdIncidencia = 1, Timestamp = DateTime.UtcNow, Asunto = "Incidencia 1", Comentario = "Comentario 1", Centro = centros[1], Aula = "Aula1", Equipo = "E11", Cerrada = true },
                new Incidencia() { IdIncidencia = 2, Timestamp = DateTime.UtcNow, Asunto = "Incidencia 2", Comentario = "Comentario 2", Centro = centros[0], Aula = "Aula2", Equipo = "E26", Cerrada = true },
                new Incidencia() { IdIncidencia = 3, Timestamp = DateTime.UtcNow, Asunto = "Incidencia 3", Comentario = "Comentario 3", Centro = centros[0], Aula = "2-1", Equipo = "E27", Cerrada = false },
                new Incidencia() { IdIncidencia = 4, Timestamp = DateTime.UtcNow, Asunto = "Incidencia 4", Comentario = "Comentario 4", Centro = centros[1], Aula = "Aula3", Equipo = "E32", Cerrada = false },
                new Incidencia() { IdIncidencia = 5, Timestamp = DateTime.UtcNow, Asunto = "Incidencia 5", Comentario = "Comentario 5", Centro = centros[2], Aula = "Aula4", Equipo = "E49", Cerrada = false },
                new Incidencia() { IdIncidencia = 6, Timestamp = DateTime.UtcNow, Asunto = "Incidencia 6", Comentario = "Comentario 6", Centro = centros[2], Aula = "Aula5", Equipo = "E50", Cerrada = false }
            });

            SaveChanges();
        }

    }
}