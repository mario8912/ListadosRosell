using Entidades.Modelos;
using System;
using System.Data.SqlClient;

namespace Entidades
{
    public class Ruta : Tabla
    {
        private const string NOMBRE = "Ruta";
        private readonly string _idRutaMax;
        private readonly string _idRutaMin;
        private readonly int _columna = 0;

        private string _query;
        public Ruta()
        {
            _query = "SELECT * FROM RUTA";
            Datos(_query);
        }

        public void Datos(string query)
        {
            using (SqlDataReader rd = base.Consulta(query))
            {
               while(rd.Read())
                { Console.WriteLine(rd.GetInt16(_columna)); }
                _conexion.Dispose();
            }   
        }
    }
}
