using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades.Global;

namespace Datos.Conexiones
{
    public class DatosConexionReporte : DatosConexion
    {
        private readonly ReportDocument _reporte;

        public DatosConexionReporte()
        {
            _reporte = GlobalInformes.ReporteCargado;
            ConectarReporte();
            Dispose();
        }

        private void ConectarReporte()
        {
            TableLogOnInfo tableInfo = new TableLogOnInfo();
            tableInfo.ConnectionInfo.ServerName = base.Servidor;
            tableInfo.ConnectionInfo.DatabaseName = base.BaseDeDatos;
            tableInfo.ConnectionInfo.IntegratedSecurity = base.SeguridadIntegrada;

            Tables tablas = _reporte.Database.Tables;

            foreach (Table tabla in tablas) tabla.ApplyLogOnInfo(tableInfo);
        }
    }
}
