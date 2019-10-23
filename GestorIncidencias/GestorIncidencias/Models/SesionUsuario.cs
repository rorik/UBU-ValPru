using Entidades;

namespace GestorIncidencias.Models
{
    public class SesionUsuario
    {
        public Centro Centro { get; set; }
        public bool EsAdmin { get; set; }
    }
}