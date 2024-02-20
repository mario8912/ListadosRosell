using Entidades;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
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
        private readonly string nombreEquipo = Environment.MachineName;

        public Conexion()
        {
            EstablecerServidorBaseDeDatos();
            FormatoCadenaConexion();
            _conexionSql = new SqlConnection(Global.CadenaConexion);
        }

        private void EstablecerServidorBaseDeDatos()
        {
            if (nombreEquipo == "PUESTO012") Servidor = "server2017";
            else Servidor = @"PUESTO012\SQLEXPRESS";

            Usuario = "sa";
            Contrasenya = "";
            BaseDeDatos = "rosell";
            SeguridadIntegrada = true;
        }
        public async Task ComprobarConexion()
        {
            using (_conexionSql = new SqlConnection(Global.CadenaConexion))
            {
                try 
                {
                    await _conexionSql.OpenAsync();
                    _conexionSql.Close();
                }
                catch (InvalidOperationException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
                catch(Exception ex) 
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString()); 
                }
                finally 
                {
                    Console.WriteLine("Cerrado");
                    this?.Dispose();  
                }
            }
        }

        public void FormatoCadenaConexion()
        {
            //Global.CadenaConexion = string.Format("Server={0};Database={1};User={2};Password={3}", Servidor, BaseDeDatos, Usuario, Contrasenya);
            Global.CadenaConexion = string.Format("Server={0};Database={1};Trusted_Connection=True;", Servidor, BaseDeDatos);
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