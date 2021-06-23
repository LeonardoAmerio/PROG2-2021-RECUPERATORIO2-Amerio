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

        public Usuario ObtenerUsuarioPorDni(int dni)
        {
            Usuario usuario = Usuarios.Single(x => x.Dni == dni);
            return usuario;
        }

        public List<Movimiento> MovimientosRealizados(int dniEnvia,int dniRecibe, string descripcion, double monto)
        {
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

                return Movimientos;
            }
            return null;
        }

        public Resultado CancelarMovimiento(int idEnvia, int idRecibe, string descripcion, double monto)
        {
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
            return new Resultado(false, "Error");
        }

        public List<Movimiento> ObtenerHistorial(int dni)
        {
            Usuario usuario = ObtenerUsuarioPorDni(dni);
            if(usuario != null)
            {
                usuario.HistoricoMovimientos.OrderByDescending(x => x.Fecha);
                return usuario.HistoricoMovimientos;
            }
            return null;
        }

       
    }
}
