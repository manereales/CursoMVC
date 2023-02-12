using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objeto = new CD_Reporte();

        public DashBoard VerDashboard()
        {
            return objeto.VerDashBoard();
        }

        public List<Reporte> VerReporte(string fechainicio, string fechafin, string idtransaccion)
        {
            return objeto.VerReporte(fechainicio, fechafin, idtransaccion);
        }
    }
}
