using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Centro
    {
        public string IdCentro { get; set; }
        public List<string> aulas { get; set; }
        public string ClaveUsuario { get; set; }
        public string ClaveAdmin { get; set; }

    }
}
