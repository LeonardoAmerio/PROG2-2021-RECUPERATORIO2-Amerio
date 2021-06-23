using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logica;

namespace RecuperatorioProgll.Models
{
    public class MovimientoResponse
    {
        public string Mensaje { get; set; }

        public MovimientoResponse(Movimiento movimiento)
        {
            this.Mensaje = $"Codigo de envio: {movimiento.Identificador}";
        }
    }
}