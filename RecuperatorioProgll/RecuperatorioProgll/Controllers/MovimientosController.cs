using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecuperatorioProgll.Models;

namespace RecuperatorioProgll.Controllers
{
    [RoutePrefix("Movimietos/{id}")]
    public class MovimientosController : ApiController
    { 

        [Route("Movimientos")]
        public IHttpActionResult Get()
        {
            return Ok(Principal.Instancia.Movimientos);
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("Movimientos")]
        public IHttpActionResult Post([FromBody]MovimientosRequest value)
        {
            List<Movimiento> movimiento = Principal.Instancia.MovimientosRealizados(value.DniEnvia, value.DniRecibe, value.Descripcion, value.Monto);
            if (movimiento != null)
                return Content(HttpStatusCode.Created, value);

            return Content(HttpStatusCode.BadRequest, value);
        }

      

        [Route("Movimientos/{dni}")]
        public IHttpActionResult Delete(int dni)
        {
            Usuario usuario = Principal.Instancia.ObtenerUsuarioPorDni(dni);
            if(usuario != null)
            {
                if (usuario.HistoricoMovimientos != null)
                {
                    List<Movimiento> movimientos = usuario.HistoricoMovimientos;
                    return Ok(movimientos);
                }
            }
            return NotFound();

        }
    }
}
