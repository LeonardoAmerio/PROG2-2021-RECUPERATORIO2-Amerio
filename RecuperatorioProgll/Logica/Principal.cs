using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Principal : IPrincipal
    { 
        public List<Movimiento> Movimientos { get; set; }
        public List<Usuario> Usuarios { get; set; }
       // public List<> Repartidores { get; set; }

        private readonly static Principal _instance = new Principal();

        private Principal()
        {
            if (Movimientos == null)
                Movimientos = new List<Movimiento>();
            if (Usuarios == null)
                Usuarios = new List<Usuario>();
            //if (Repartidores == null)
             //   Repartidores = new List<>();
        }

        public static Principal Instancia
        {
            get
            {
                return _instance;
            }
        }

        //ESTE METODO PODRIA NO EXISTIR YA QUE TIENE 1 SOLA SENTENCIA
        public Usuario ObtenerUsuarioPorDni(int dni)
        {
            //SI EL DNI ES INCORRECTO ESTO GENERA UNA EXCEPCION, DEBERIA SER SINGLEORDEFAULT
            Usuario usuario = Usuarios.Single(x => x.Dni == dni);
            return usuario;
        }

        //EL NOMBRE DEL METODO NO COINCIDE CON LO QUE HACE. ES UN PROBLEMA DE MANTENIBILIDAD A FUTURO
        //PODRIA DEVOLVER RESULTADO COMO OBJETO, ASI PODRIAS INFORMAR EL PROBLEMA EN CASO DE VALIDACION
        public List<Movimiento> MovimientosRealizados(int dniEnvia,int dniRecibe, string descripcion, double monto)
        {
            //EL FILTRO DE USUARIOS ESTA AL REVES.
            Usuario usuarioReceptor = Usuarios.Single(x => x.Dni == dniEnvia);
            Usuario usuarioTransmisor = Usuarios.Single(x => x.Dni == dniRecibe);

            if ((usuarioReceptor == null) || (usuarioTransmisor == null))
                return null;

            if(usuarioTransmisor.Saldo >= monto)
            {
                usuarioTransmisor.Saldo -= monto;
                usuarioReceptor.Saldo += monto;
                Movimiento transaccion = new Movimiento(descripcion, monto * -1);
                usuarioTransmisor.HistoricoMovimientos.Add(transaccion);
                Movimientos.Add(transaccion);
                Movimiento transaccionRecibida = new Movimiento(descripcion, monto);
                usuarioReceptor.HistoricoMovimientos.Add(transaccion);
                Movimientos.Add(transaccionRecibida);

                //SI BIEN NO ESTA MAL, HAY UN PROBLEMA DE DISEÑO. ESTE METODO TIENE LOGICA QUE LE CORRESPONDE A USUARIO.
                //LO IDEAL SERIA QUE EL USUARIO TENGA UN METODO "CREARTRANSACCION" QUE SE ENCARGUE AL MISMO TIEMPO DE AGREGAR A SU LISTA Y 
                //DESCONTAR EL SALDO.

                return Movimientos;
            }
            return null;
        }

        //LA DESCRIPCION Y EL MONTO NO SON NECESARIOS. EL QUE CANCELA SOLO SABE LOS IDS.
        public Resultado CancelarMovimiento(int idEnvia, int idRecibe, string descripcion, double monto)
        {
            //MONTO Y DESCRIPCION SALEN DE ESTOS MOVIMIENTOS ORIGINALES
            Movimiento movimiento = Movimientos.Find(x => x.Identificador == idEnvia);
            Movimiento movimiento2 = Movimientos.Find(x => x.Identificador == idRecibe);

            if(movimiento != null)
            {
                Movimiento invertirMonto = new Movimiento(descripcion, monto);
                Movimientos.Add(invertirMonto);
                return new Resultado(true, $"Cancelacion: {descripcion}");

            }
            if (movimiento2 != null)
            {
                Movimiento invertir = new Movimiento(descripcion, monto * -1);
                Movimientos.Add(invertir);
                return new Resultado(true, $"Cancelacion: {descripcion}");
            }

            //INFORMAR CUAL ES EL ERROR QUE SE PRODUCE, LA PALABRA "ERROR" NO LE SIRVE AL CLIENTE PARA SABER QUE PASO.
            return new Resultado(false, "Error");
        }

        public List<Movimiento> ObtenerHistorial(int dni)
        {
            Usuario usuario = ObtenerUsuarioPorDni(dni);
            if(usuario != null)
            {
                //ASI PUESTO NO ORDENA, LE FALTA LA ASIGNACION A LA LISTA O A OTRA VARIABLE
                usuario.HistoricoMovimientos.OrderByDescending(x => x.Fecha);
                return usuario.HistoricoMovimientos;
            }
            return null;
        }

       
    }
}
