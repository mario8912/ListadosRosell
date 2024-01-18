using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using Datos;


namespace Negocio
{
    public static class NegocioParametrosReporte
    {
        public static void GenerarTxtParamentros(ReportDocument reporte, string rutaReporte)
        { 
            DatosParametrosReporte parametrosReporte = new DatosParametrosReporte(reporte, rutaReporte);
            parametrosReporte.GeneraTxtParamentrosReporte();
        }
    }
}
