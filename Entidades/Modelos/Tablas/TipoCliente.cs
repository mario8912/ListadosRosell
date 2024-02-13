using Entidades.Modelos;
using System.Data.SqlClient;

namespace Tablas
{
    public class TipoCliente : Tabla
    {
        private const string NOMBRE_TABLA = "TipoCliente";
        private const string COLUMNA_ID = "idTipoCliente";
        public string IdTipoClienteMax { get; set; }
        public string IdTipoClienteMin { get; set; }

        private readonly int _columna = 0;
        private readonly string _query;
        private string _resultadoQuery;
        public TipoCliente()
        {
            _query =
                $"SELECT MIN({COLUMNA_ID})" +
                $" FROM {NOMBRE_TABLA}";

            Datos(_query);
            IdTipoClienteMin = _resultadoQuery;

            _query =
                $"SELECT MAX({COLUMNA_ID})" +
                $" FROM {NOMBRE_TABLA}";

            Datos(_query);
            IdTipoClienteMax = _resultadoQuery;
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

