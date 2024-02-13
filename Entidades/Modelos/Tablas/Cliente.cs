using Entidades.Modelos;
using System.Data.SqlClient;

namespace Tablas
{
    public class Cliente : Tabla
    {
        private const string NOMBRE_TABLA = "Cliente";
        private const string COLUMNA_ID = "idCliente";
        public string IdClienteMax { get; set; }
        public string IdClienteMin { get; set; }

        private readonly int _columna = 0;
        private readonly string _query;
        private string _resultadoQuery;
        public Cliente()
        {
            _query =
                $"SELECT MIN({COLUMNA_ID}) " +
                $" FROM {NOMBRE_TABLA}" +
                $" WHERE eliminado = 0";
            
            Datos(_query);
            IdClienteMin = _resultadoQuery;

            _query = 
                $"SELECT MAX({COLUMNA_ID}) " +
                $" FROM {NOMBRE_TABLA} " +
                $" WHERE eliminado = 0";

            Datos(_query);
            IdClienteMax = _resultadoQuery;
        }
        protected void Datos(string query)
        {
            using (SqlDataReader reader = base.Consulta(query))
            {
                reader.Read();
                _resultadoQuery = reader.GetInt16(_columna).ToString();
                _conexion.Dispose();
            }
        }
    }
}

