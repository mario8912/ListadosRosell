using Entidades.Modelos;
using System.Data.SqlClient;

namespace Tablas
{
    public class Articulo : Tabla
    {
        private const string NOMBRE_TABLA = "Articulo";
        private const string COLUMNA_ID = "idArticulo";
        public string IdArticuloMax { get; set; }
        public string IdArticuloMin { get; set; }

        private readonly int _columna = 0;
        private readonly string _query;
        private string _resultadoQuery;
        public Articulo()
        {
            _query =
                $"SELECT MIN({COLUMNA_ID}) " +
                $" FROM {NOMBRE_TABLA}";
                //$" WHERE {COLUMNA_ID} > \'.\'";

            Datos(_query);
            IdArticuloMin = _resultadoQuery;

            _query =
                $"SELECT MAX({COLUMNA_ID}) " +
                $" FROM {NOMBRE_TABLA}";
                //$" WHERE {COLUMNA_ID} > \'.\'";

            Datos(_query);
            IdArticuloMax = _resultadoQuery;
        }

        protected void Datos(string query)
        {
            using (SqlDataReader reader = base.Consulta(query))
            {
                reader.Read();
                _resultadoQuery = reader.GetString(_columna).ToString();
                _conexion.Dispose();
            }
        }
    }
}

