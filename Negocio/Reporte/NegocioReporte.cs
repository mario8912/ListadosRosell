using CrystalDecisions.CrystalReports.Engine;
using Entidades.Global;
using Datos.Conexiones;
using Entidades.Modelos.Reporte;
using System.IO;

namespace Negocio.Reporte
{
    public static class NegocioReporte
    {
        private static EntidadReporte _reporte;
        private static string _rutaReporte;

        public static ReportDocument CargarReporte(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            _reporte = new EntidadReporte();

            if (GlobalReporteNoEstaCargado())
            {
                AsignarValoresReporte();
                Cargar();
                Conectar();
            } 
            
            return null;
        }

        private static bool GlobalReporteNoEstaCargado()
        {
            if (GlobalInformes.ReporteCargado == null || GlobalInformes.RutaReporte != _rutaReporte) return true;
            else return false;
        }

        private static void AsignarValoresReporte()
        {
            _reporte.RutaReporte = _rutaReporte;
            _reporte.NombreReporte = Path.GetFileName(_rutaReporte);
            _reporte.Reporte = _reporte;

            GlobalInformes.RutaReporte = _rutaReporte;
        }

        private static void Cargar()
        {
            _reporte.Reporte.Load(GlobalInformes.RutaReporte);
            GlobalInformes.ReporteCargado = _reporte;
        }

        private static void Conectar()
        {
            _ = new DatosConexionReporte().ComprobarConexion();
        }

        public static bool ComprobarParametrosReporte()
        {
            return GlobalInformes.ReporteCargado.ParameterFields.Count > 0;
        }

        public static void ImprimirReporte()
        {
            new EntidadReporte().ImprimirReporte();
        }
    }
}
