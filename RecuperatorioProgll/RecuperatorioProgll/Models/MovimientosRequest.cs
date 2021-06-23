using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperatorioProgll.Models
{
    public class MovimientosRequest
    {
        public int DniEnvia { get; set; }
        public int DniRecibe { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }

        public List<Movimiento> CrearMovimiento()
        {
            Usuario usuarioRecibe = Principal.Instancia.ObtenerUsuarioPorDni(DniRecibe);
            Usuario usuarioEnvia = Principal.Instancia.ObtenerUsuarioPorDni(DniEnvia);
            List<Movimiento> movimiento = Principal.Instancia.MovimientosRealizados(usuarioEnvia.Dni, usuarioRecibe.Dni, this.Descripcion, this.Monto);
            return movimiento;
        }
    }
}