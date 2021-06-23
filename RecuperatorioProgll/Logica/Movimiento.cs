using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Movimiento
    {
        public int Identificador { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }

        public Movimiento(string descripcion, double monto)
        {
            Random random = new Random();
            this.Identificador = random.Next(1, 10000);
            this.Fecha = DateTime.Today;
            this.Descripcion = descripcion;
            this.Monto = monto;
        }

    }
   
}
