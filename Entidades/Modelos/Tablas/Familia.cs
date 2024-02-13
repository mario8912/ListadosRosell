using Entidades.Modelos;
using System.Data.SqlClient;

namespace Tablas
{
    public class Familia : Tabla
    {
        private const string NOMBRE_TABLA = "Familia";
        private const string COLUMNA_ID = "idFamilia";
        public string IdFamiliaMax { get; set; }
        public string IdFamiliaMin { get; set; }

        private readonly int _columna = 0;
        private readonly string _query;
        private string _resultadoQuery;
        public Familia()
        {
            _query = 
                $"SELECT MIN({COLUMNA_ID})" +
                $" FROM {NOMBRE_TABLA}";

            Datos(_query);
            IdFamiliaMin = _resultadoQuery;

            _query = 
                $"SELECT MAX({COLUMNA_ID})" +
                $" FROM {NOMBRE_TABLA}";

            Datos(_query);
            IdFamiliaMax = _resultadoQuery;
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

