using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Data.SqlClient;

namespace Datos
{
    public class ConexionReporte : Conexion
    {
        private readonly ReportDocument _reporte;
        
        public ConexionReporte(ReportDocument reporte)
        {
            _reporte = reporte;
            ConectarReporte();
        }

        private void ConectarReporte()
        {
            TableLogOnInfo tableInfo = new TableLogOnInfo();
            tableInfo.ConnectionInfo.ServerName = Servidor;
            tableInfo.ConnectionInfo.DatabaseName = BaseDeDatos;
            tableInfo.ConnectionInfo.IntegratedSecurity = SeguridadIntegrada;
            #region CONEXIÓN LOCAL
            /*tableInfo.ConnectionInfo.UserID = Usuario;
            tableInfo.ConnectionInfo.Password = Contrasenya;*/
            #endregion
            Tables tablas = _reporte.Database.Tables;
            
            foreach (Table tabla in tablas)
            {
                var tablaNombre = tabla.Name;
                var esProcedimiento = tablaNombre.Substring(tablaNombre.Length - 1);

                if (esProcedimiento == "1")
                {
                    Console.WriteLine(tablaNombre);
                }
                tabla.ApplyLogOnInfo(tableInfo);
                //tabla.SetDataSource(tableInfo);
            }
        }
    }
}
