using CrystalDecisions.CrystalReports.Engine;
using Entidades.Global;
using Datos.Conexiones;
using Entidades.Modelos.Reporte;
using System.IO;

namespace Negocio.Reporte
{
    public class NegocioReporte
    {
        //DI
        private readonly GlobalInformes _globalInformes;

        private EntidadReporte _reporte;
        private string _rutaReporte;

        public NegocioReporte(GlobalInformes globalInformes)
        {
            _globalInformes = globalInformes;
        }

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
            if (_globalInformes.ReporteCargado == null || _globalInformes.RutaReporte != _rutaReporte) return true;
            else return false;
        }

        private void AsignarValoresReporte()
        {
            _reporte.RutaReporte = _rutaReporte;
            _reporte.NombreReporte = Path.GetFileName(_rutaReporte);
            _reporte.Reporte = _reporte;

            _globalInformes.RutaReporte = _rutaReporte;
        }

        private void Cargar()
        {
            _reporte.Reporte.Load(_globalInformes.RutaReporte);
            _globalInformes.ReporteCargado = _reporte;
        }

        private void Conectar()
        {
            _ = new DatosConexionReporte(_globalInformes).ComprobarConexion();
        }

        public bool ComprobarParametrosReporte()
        {
            return _globalInformes.ReporteCargado.ParameterFields.Count > 0;
        }

        public void ImprimirReporte()
        {
            new EntidadReporte().ImprimirReporte();
        }
    }
}
