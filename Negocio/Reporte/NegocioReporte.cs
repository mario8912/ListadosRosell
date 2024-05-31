using CrystalDecisions.CrystalReports.Engine;
using Entidades.Global;
using Datos.Conexiones;
using Entidades.Modelos.Reporte;
using System.IO;

namespace Negocio.Reporte
{
    public class NegocioReporte
    {
        private EntidadReporte _reporte;
        private string _rutaReporte;

        public ReportDocument CargarReporte(string rutaReporte)
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

        private bool GlobalReporteNoEstaCargado()
        {
            if (GlobalInformes.ReporteCargado == null || GlobalInformes.RutaReporte != _rutaReporte) return true;
            else return false;
        }

        private void AsignarValoresReporte()
        {
            _reporte.RutaReporte = _rutaReporte;
            _reporte.NombreReporte = Path.GetFileName(_rutaReporte);
            _reporte.Reporte = _reporte;

            GlobalInformes.RutaReporte = _rutaReporte;
        }

        private void Cargar()
        {
            _reporte.Reporte.Load(GlobalInformes.RutaReporte);
            GlobalInformes.ReporteCargado = _reporte;
        }

        private void Conectar()
        {
            _ = new DatosConexionReporte().ComprobarConexion();
        }

        public bool ComprobarParametrosReporte()
        {
            return GlobalInformes.ReporteCargado.ParameterFields.Count > 0;
        }

        public void ImprimirReporte()
        {
            new EntidadReporte().ImprimirReporte();
        }
    }
}
