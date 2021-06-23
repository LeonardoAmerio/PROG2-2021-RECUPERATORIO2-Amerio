using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Usuario
    {
       
        public string NombreApellido { get; set; }
        public int Dni { get; set; }
        public double Saldo { get; set; }
        public List<Movimiento> HistoricoMovimientos { get; set; }

    }
}
