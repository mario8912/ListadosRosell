
using Datos;
using System;
using System.Data.SqlClient;

namespace Entidades.Modelos
{
    public abstract class Tabla
    { 
        protected Conexion _conexion;

        public virtual SqlDataReader Consulta(string query)
        {
            _conexion = new Conexion();
            _conexion.AbrirConexion();

            using (SqlCommand commando = new SqlCommand(query, _conexion._conexionSql))
            {
                SqlDataReader dataReader = commando.ExecuteReader();
                return dataReader;
            }
            
        }
    }
}
