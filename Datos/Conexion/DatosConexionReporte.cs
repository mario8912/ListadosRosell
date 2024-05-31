using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades.Global;
using Microsoft.Extensions.DependencyInjection;

namespace Datos.Conexiones
{
    public class DatosConexionReporte : DatosConexion
    {
        //DI
        private readonly GlobalInformes _globalInformes;

        private readonly ReportDocument _reporte;

        public DatosConexionReporte(GlobalInformes globalInformes) : base(globalInformes)        
        {
                _globalInformes = globalInformes;
                _reporte = _globalInformes.ReporteCargado;
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
