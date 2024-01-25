using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Datos
{
    public class ConexionReporte
    {
        private const string servidor = @"DESKTOP-BO267HF\SQLEXPRESS";
        private const string baseDeDatos = "ROSELL";
        private string cadenaConexion = string.Format("Server={0};Database={1};Trusted_Connection = True;", servidor, baseDeDatos);
        private ReportDocument _reporte;
        private TableLogOnInfo _conexionInfo;

        public ConexionReporte(ReportDocument reporte)
        {
            _reporte = reporte;
            if(ComprobarConexion()) ConectarReporte();
        }   

        private bool ComprobarConexion()
        {
            SqlConnection conexion = new SqlConnection();

            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConectarReporte()
        {
            _conexionInfo = new TableLogOnInfo();
            _conexionInfo.ConnectionInfo.ServerName = servidor;
            _conexionInfo.ConnectionInfo.DatabaseName = baseDeDatos;
            _conexionInfo.ConnectionInfo.IntegratedSecurity = true;

            Tables tablas = _reporte.Database.Tables;

            foreach (Table tabla in tablas)
            {
                tabla.ApplyLogOnInfo(_conexionInfo);
            }
        }
    }
}
