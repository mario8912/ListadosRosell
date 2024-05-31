using Entidades.Global;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("UnitTestProject")]
namespace Datos.Conexiones
{
    public class DatosConexion : IDisposable 
    {
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

            ConexionSql = new SqlConnection(GlobalInformes.CadenaConexion);
        }

        private void EstablecerServidorBaseDeDatos()
        {
            #region JSON
            /*var datosJson = DatosLeerJson.DatosConexionJson();

            Servidor = datosJson.Servidor;
            Usuario = datosJson.Seguridad.Usuario;
            Contrasenya = datosJson.Seguridad.Contrasenya;
            BaseDeDatos = datosJson.BaseDeDatos;
            SeguridadIntegrada = datosJson.Seguridad.TrustedConnection;*/
            #endregion

            Servidor = "DESKTOP-BO267HF\\SQLEXPRESS";
            BaseDeDatos = "rosell";
            SeguridadIntegrada = true;
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
            GlobalInformes.CadenaConexion = string.Format("Server={0};Database={1};Trusted_Connection={2};", Servidor, BaseDeDatos, SeguridadIntegrada);
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