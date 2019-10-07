using Entidades;
using System.Linq;

namespace GestorIncidencias.Models
{
    public interface IContextoIncidencias
    {
        IQueryable<Incidencia> Incidencias { get; }
        IQueryable<Centro> Centros { get; }
    }
}