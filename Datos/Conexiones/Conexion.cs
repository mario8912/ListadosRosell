using System;
using System.Data.SqlClient;
using Entidades;
using Microsoft.SqlServer.Server;

namespace Datos
{
    public class Conexion
    {
        public string Servidor {get; set;}
        public string BaseDeDatos { get; set;}
        public bool SeguridadIntegrada { get; set;}
        public string Usuario { get; set;}
        public string Contrasenya { get; set;}
        private readonly string nombreEquipo = Environment.MachineName;
        //public string EstadoConexion { get; set; }

        public Conexion()
        {
            EstablecerServidorBaseDeDatos();
            ComprobarConexion();
        }

        private void EstablecerServidorBaseDeDatos()
        {
            if (nombreEquipo == "PUESTO012") Servidor = "server2017";
            else Servidor = @"DESKTOP-BO267HF\SQLEXPRESS";

            Usuario = "sa";
            Contrasenya = "";
            BaseDeDatos = "rosell";
            SeguridadIntegrada = true;
        }
        private void ComprobarConexion()
        {
            SqlConnection conexion = new SqlConnection
            {
                ConnectionString = FormatoCadenaConexion()
            };

            try 
            { 
                conexion.Open();
                Console.WriteLine("CONEXION");
            }
            catch (Exception excepcion) 
            { 
                throw new Exception("Error al conectarse con la base de datos:" + Environment.NewLine + excepcion.Message); 
            }
            finally 
            { 
                conexion.Close(); 
            }
        }

        private string FormatoCadenaConexion()
        {
            //return string.Format("Server={0};Database={1};User={2};Password={3}", Servidor, BaseDeDatos, Usuario, Contrasenya);
            
            return string.Format("Server={0};Database={1};Trusted_Connection=True;", Servidor, BaseDeDatos);
        }

    }
}