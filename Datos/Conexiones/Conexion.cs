using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Datos
{
    public class Conexion
    {
        private string baseDatos;
        private string servidor;
        /*
         *private string usuario;
         *private string clave;
         *private string path;
        */
        private static Conexion conexion1 = null;

        private Conexion()
        {
            this.baseDatos = "miBaseDeDatos";
            this.servidor = @"DESKTOP-BO267HF\SQLEXPRESS";

            /* Diferentes propiedades de la conexión
             * this.path = @"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\miBaseDeDatos.mdf";
             *this.usuario = "sa";
             *this.clave = "2891998";
             *Server = localhost\SQLEXPRESS; Database = master; Trusted_Connection = True;
            */
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection cadenaConexion = new SqlConnection();
            
            try
            {
                cadenaConexion.ConnectionString =
                    "Server=" + this.servidor + ";" +
                    //"Authentication=Windows Authentication;" +
                    "Database=" + this.baseDatos + ";" +
                    "Trusted_Connection = True;";
            }
            catch (Exception)
            {
                cadenaConexion = null;
                throw;
            }

            return cadenaConexion;
        }

        public static Conexion ComprobarConexion()
        {
            return conexion1 == null ? new Conexion() : null;
        }
    }

    
}
    