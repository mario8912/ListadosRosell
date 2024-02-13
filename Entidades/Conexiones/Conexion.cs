using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion : IDisposable
    {
        public string Servidor {get; set;}
        public string BaseDeDatos { get; set;}
        public bool SeguridadIntegrada { get; set;}
        public string Usuario { get; set;}
        public string Contrasenya { get; set;}
        public SqlConnection _conexionSql;

        private string _cadenaConexion;

        private readonly string nombreEquipo = Environment.MachineName;

        public Conexion()
        {
            EstablecerServidorBaseDeDatos();
            _cadenaConexion = FormatoCadenaConexion();
            _conexionSql = new SqlConnection(_cadenaConexion);
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

            using (_conexionSql = new SqlConnection(connectionString))
            {
                try
                {
                    await _conexionSql.OpenAsync();
                }
                finally
                {
                    Dispose();
                }
            }
        }

        public string FormatoCadenaConexion()
        {
            //return string.Format("Server={0};Database={1};User={2};Password={3}", Servidor, BaseDeDatos, Usuario, Contrasenya);
            return _cadenaConexion = string.Format("Server={0};Database={1};Trusted_Connection=True;", Servidor, BaseDeDatos);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_conexionSql != null)
                {
                    _conexionSql.Dispose();
                    _conexionSql = null;
                }
            }
        }

        public void AbrirConexion()
        {
            _conexionSql.Open();
        }
    }
}