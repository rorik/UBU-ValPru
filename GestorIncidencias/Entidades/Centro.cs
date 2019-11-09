using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entidades
{
    public class Centro
    {
        [NotMapped]
        public const char AulasSeparator = ';';

        [Key]
        public string IdCentro { get; set; }

        public string Aulas { get; set; }

        [NotMapped]
        public string[] ListaAulas
        {
            get { return Aulas?.Split(AulasSeparator).Where(a => !string.IsNullOrEmpty(a)).ToArray(); }
            set
            {
                if (value != null)
                {
                    Aulas = string.Join(AulasSeparator.ToString(), value);
                }
                else
                {
                    Aulas = null;
                }
            }
        }

        public string ClaveUsuario { get; set; }

        public string ClaveAdmin { get; set; }

        public byte[] SaltUsuario { get; set; }

        public byte[] SaltAdmin { get; set; }

    }
}
