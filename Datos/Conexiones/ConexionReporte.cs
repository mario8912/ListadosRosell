using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Datos
{
    public class ConexionReporte
    {
        private readonly ReportDocument _reporte;
        private const bool _seguridadIntegrada = true;
        private Conexion _conexion;
        
        public ConexionReporte(ReportDocument reporte)
        {
            _reporte = reporte;
            _conexion = new Conexion();
            ConectarReporte();
        }

        private void ConectarReporte()
        {
            TableLogOnInfo tableInfo = new TableLogOnInfo();
            tableInfo.ConnectionInfo.ServerName = _conexion.Servidor;
            tableInfo.ConnectionInfo.DatabaseName = _conexion.BaseDeDatos;
            tableInfo.ConnectionInfo.IntegratedSecurity = _seguridadIntegrada;

            Tables tablas = _reporte.Database.Tables;

            foreach (Table tabla in tablas)
            {
                tabla.ApplyLogOnInfo(tableInfo);
            }
        }
    }
}
