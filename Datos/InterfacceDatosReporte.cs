using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;

namespace Datos
{
    public interface InterfacceDatosReporte
    {
         void CargarReporte();
         void ImprimirReporte();
    }
}