using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Resultado
    {
        public bool EstaBien { get; set; }
        public string Mensaje { get; set; }
      

        public Resultado(bool ok, string mensaje)
        {
            this.EstaBien = ok;
            this.Mensaje = mensaje;
        }

    
    }
}
