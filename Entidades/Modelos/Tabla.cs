
using Datos;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Entidades.Modelos
{
    public class Tabla : Conexion
    {
        
        public SqlDataReader GetMinField(string campo, string tabla)
        {
            return SqlConexionQuery(campo, tabla, false);
        }

        public SqlDataReader GetMaxField(string campo, string tabla)
        {
            return SqlConexionQuery(campo, tabla, true);
        }
        public SqlDataReader SqlConexionQuery(string campo, string tabla, bool maxMin)
        {
            string query = string.Format("SELECT {0}({1}) FROM {2}", maxMin, campo, tabla);

            try
            {
                using (SqlCommand comando = new SqlCommand(query, base._conexion))
                {
                    AbrirConexion();
                    SqlDataReader reader = comando.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw new Exception(ex.Message);
            }
        }
    }
}
