using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion
    {
        public string Servidor {get; set;}
        public string BaseDeDatos { get; set;}
        public bool SeguridadIntegrada { get; set;}
        public string Usuario { get; set;}
        public string Contrasenya { get; set;}
        internal SqlConnection _conexion;

        private string _cadenaConexion;

        private readonly string nombreEquipo = Environment.MachineName;

        public Conexion()
        {
            EstablecerServidorBaseDeDatos();
            FormatoCadenaConexion();
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
        public async Task ComprobarConexion()
        {
            string connectionString = _cadenaConexion;

            using (_conexion = new SqlConnection(connectionString))
            {
                try
                {
                    await _conexion.OpenAsync();
                    Console.WriteLine("Connected");
                }
                finally
                {
                    CerrarConexion();
                }
            }
        }

        private void FormatoCadenaConexion()
        {
            //return string.Format("Server={0};Database={1};User={2};Password={3}", Servidor, BaseDeDatos, Usuario, Contrasenya);
            _cadenaConexion = string.Format("Server={0};Database={1};Trusted_Connection=True;", Servidor, BaseDeDatos);
        }

        public void AbrirConexion()
        {
            FormatoCadenaConexion();
            using (_conexion = new SqlConnection(_cadenaConexion))
            {
                _conexion.Open();
            }       
        }
        public void CerrarConexion()
        {
            _conexion.Close();
        }
    }
}