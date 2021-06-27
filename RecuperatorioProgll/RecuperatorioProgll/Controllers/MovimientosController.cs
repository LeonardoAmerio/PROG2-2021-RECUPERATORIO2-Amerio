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
    //NO ES NECESARIO EN ESTE CASO, Y PUEDE GENERAR ERRORES CUANDO NO RECIBE EL ID EN ALGUN METODO DEL CONTROLLER
    [RoutePrefix("Movimietos/{id}")]
    public class MovimientosController : ApiController
    { 
        //NO ES LO QUE PIDE EL ENUNCIADO. SE PEDIA LOS MOVIMIENTOS DE UN USUARIO. USANDO EL DNI COMO FILTRO
        [Route("Movimientos")]
        public IHttpActionResult Get()
        {
            return Ok(Principal.Instancia.Movimientos);
        }

        // ELIMINAR LO QUE NO SE USA
        public string Get(int id)
        {
            return "value";
        }

        [Route("Movimientos")]
        public IHttpActionResult Post([FromBody]MovimientosRequest value)
        {
            //PROBLEMA DE DISEÑO, DEBERIA CREAR UNA INSTANCIA "MOVIMIENTO" Y PASAR ESO COMO PARAMETRO, NO LOS VALORES POR SEPARADO.
            List<Movimiento> movimiento = Principal.Instancia.MovimientosRealizados(value.DniEnvia, value.DniRecibe, value.Descripcion, value.Monto);

            //NO ES NECESARIO DEVOLVER VALUE. SE DEBE DEVOLVER LO QUE SE CREO, O BIEN LA VALIDACION HECHA. RETORNAR LO MISMO QUE EL CLIENTE ENVIA
            //PUEDE GENERAR CONFUSIONES.
            if (movimiento != null)
                return Content(HttpStatusCode.Created, value);

            //EL STATUS CODE ESTA BIEN, PERO NO DEVUELVE EL MENSAJE DE ERROR
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
