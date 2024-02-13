using Entidades.Modelos;
using System.Data.SqlClient;

namespace Tablas
{
    public class Preventa : Tabla
    {
        private const string NOMBRE_TABLA = "Preventista";
        private const string COLUMNA_ID = "idPreventa";
        public string IdPreventaMax { get; set; }
        public string IdPreventaMin { get; set; }

        private readonly int _columna = 0;
        private readonly string _query;
        private string _resultadoQuery;
        public Preventa()
        {
            _query = $"SELECT MIN({COLUMNA_ID}) " +
                $"FROM {NOMBRE_TABLA}" +
                $"WHERE inactivo = 0";

            Datos(_query);
            IdPreventaMin = _resultadoQuery;

            _query = $"SELECT MAX({COLUMNA_ID}) " +
                $"FROM {NOMBRE_TABLA}" +
                $"WHERE inactivo = 0";

            Datos(_query);
            IdPreventaMax = _resultadoQuery;
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

