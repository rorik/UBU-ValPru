using Entidades;
using System.Data.Common;
using System.Data.Entity;

namespace GestorIncidencias.Models
{
    public abstract class ContextoIncidencias : DbContext
    {
        public ContextoIncidencias() {}
        public ContextoIncidencias(DbConnection connection) : base(connection, false) { }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Centro> Centros { get; set; }        
    }
}