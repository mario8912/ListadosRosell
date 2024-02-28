using Entidades.Global;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Datos.Conexiones
{
    public class ConexionReporte : Conexion
    {
        private readonly ReportDocument _reporte;

        public ConexionReporte()
        {
            _reporte = GlobalInformes.ReporteCargado;
            ConectarReporte();
            Dispose();
        }

        private void ConectarReporte()
        {
            TableLogOnInfo tableInfo = new TableLogOnInfo();
            tableInfo.ConnectionInfo.ServerName = Servidor;
            tableInfo.ConnectionInfo.DatabaseName = BaseDeDatos;
            tableInfo.ConnectionInfo.IntegratedSecurity = SeguridadIntegrada;

            #region CONEXIÓN LOCAL
            tableInfo.ConnectionInfo.UserID = Usuario;
            tableInfo.ConnectionInfo.Password = Contrasenya;
            #endregion

            Tables tablas = _reporte.Database.Tables;

            foreach (Table tabla in tablas)
            {
                tabla.ApplyLogOnInfo(tableInfo);
                tabla.SetDataSource(tableInfo);
            }
        }
    }
}
