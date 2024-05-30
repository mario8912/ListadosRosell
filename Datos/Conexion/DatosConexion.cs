using Datos.Conexion;
using Entidades.Global;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Datos.Conexiones
{
    public class DatosConexion : IDisposable 
    {
        private GlobalInformes _globalInformes;
        public string Servidor {get; private set;}
        public string BaseDeDatos { get; private set;}
        public bool SeguridadIntegrada { get; private set;}
        public string Usuario { get; private set;}
        public string Contrasenya { get; private set;}
        public SqlConnection ConexionSql { get; private set; }

        public DatosConexion()
        {
            EstablecerServidorBaseDeDatos();
            FormatoCadenaConexion();

            _globalInformes = new GlobalInformes();
            ConexionSql = new SqlConnection(_globalInformes.CadenaConexion);
        }

        private void EstablecerServidorBaseDeDatos()
        {
            var datosJson = DatosLeerJson.DatosConexionJson();

            Servidor = datosJson.Servidor;
            Usuario = datosJson.Seguridad.Usuario;
            Contrasenya = datosJson.Seguridad.Contrasenya;
            BaseDeDatos = datosJson.BaseDeDatos;
            SeguridadIntegrada = datosJson.Seguridad.TrustedConnection;
        }

        public async Task<bool> ComprobarConexion()
        {
            using (DatosConexion cn = new DatosConexion())
            {
                try
                {
                    await cn.ConexionSql.OpenAsync();
                    return true; 
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public void FormatoCadenaConexion()
        {
            _globalInformes.CadenaConexion = string.Format("Server={0};Database={1};Trusted_Connection=True;", Servidor, BaseDeDatos);
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
                if (ConexionSql != null)
                {
                    ConexionSql.Dispose();
                    ConexionSql = null;
                }
            }
        }

        public void AbrirConexion()
        {
            ConexionSql.OpenAsync();
        }

        public string EjecutarConsulta(string consulta)
        {
            using (DatosConexion cn = new DatosConexion())
            {
                var respuesta = "";

                cn.AbrirConexion();

                using (SqlCommand commando = new SqlCommand(consulta, cn.ConexionSql))
                {
                    SqlDataReader reader = commando.ExecuteReader();
                    reader.Read();
                    respuesta = reader.GetValue(0).ToString();

                    reader.Close();
                    cn.Dispose();

                    return respuesta;
                }
            }
        }
    }
}