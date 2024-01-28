﻿using System;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class Conexion
    {
        public string Servidor {get; set;}
        public string BaseDeDatos { get; set;}
        public string EstadoConexion { get; set; }

        public Conexion()
        {
            EstablecerServidorBaseDeDatos();
            ComprobarConexion();
        }

        private void EstablecerServidorBaseDeDatos()
        {
            Servidor = @"DESKTOP-BO267HF\SQLEXPRESS";
            BaseDeDatos = "ROSELL";
        }
        private void ComprobarConexion()
        {
            SqlConnection conexion = new SqlConnection
            {
                ConnectionString = FormatoCadenaConexion()
            };

            try { conexion.Open(); }
            catch (Exception excepcion) { throw new Exception("Error al conectarse con la base de datos:" + Environment.NewLine + excepcion.Message); }
            finally { conexion.Close(); }
        }

        private string FormatoCadenaConexion()
        {
            return string.Format("Server={0};Database={1};Trusted_Connection=True;", Servidor, BaseDeDatos);
        }

    }
}