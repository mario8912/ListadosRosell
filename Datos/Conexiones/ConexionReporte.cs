﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Data.SqlClient;

namespace Datos
{
    public class ConexionReporte
    {
        //private const string servidor = @"SERVER2017";
        private const string baseDeDatos = "ROSELL2022";
        private string cadenaConexion = string.Format("Server={0};Database={1};Trusted_Connection = True;", servidor, baseDeDatos);
        private ReportDocument _reporte;
        private TableLogOnInfo _conexionInfo;

        private SqlConnection _conexion;

        public ConexionReporte(ReportDocument reporte)
        {
            _reporte = reporte;
            if(ComprobarConexion()) ConectarReporte();

            _conexion.Close();
        }   

        private bool ComprobarConexion()
        {
            _conexion = new SqlConnection
            {
                ConnectionString = cadenaConexion
            };

            try
            {
                _conexion.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
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
