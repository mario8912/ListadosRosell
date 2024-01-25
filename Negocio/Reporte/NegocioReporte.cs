using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using Datos;

namespace Negocio
{
    public static class NegocioReporte
    {
        public static void ImprimirReporte(string rutaReporte)
        {
            Reporte reporte = new Reporte(rutaReporte);
            reporte.ImprimirReporte();
        }
    }
}
