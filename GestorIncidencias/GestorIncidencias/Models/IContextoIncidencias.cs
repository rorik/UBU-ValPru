using Entidades;
using System.Linq;

namespace GestorIncidencias.Models
{
    public interface IContextoIncidencias
    {
        IQueryable<Incidencia> Indicencias { get; set; }
        IQueryable<Centro> Centros { get; set; }
    }
}