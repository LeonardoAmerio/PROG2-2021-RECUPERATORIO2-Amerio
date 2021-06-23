using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public interface IPrincipal
    {
        Usuario ObtenerUsuarioPorDni(int dni);
        List<Movimiento> MovimientosRealizados(int dniEnvia, int dniRecibe, string descripcion, double monto);
        Resultado CancelarMovimiento(int idEnvia, int idRecibe, string descripcion, double monto);
        List<Movimiento> ObtenerHistorial(int dni);
    }
}
