using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Centro
    {
        [Key]
        public string IdCentro { get; set; }
        public string Aulas { get; set; }
        public string ClaveUsuario { get; set; }
        public string ClaveAdmin { get; set; }
        public byte[] SaltUsuario { get; set; }
        public byte[] SaltAdmin { get; set; }

    }
}
