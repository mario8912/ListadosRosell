using Entidades.Modelos;
using System.Data.SqlClient;

namespace Tablas
{
    public class Ruta : Tabla
    {
        private const string NOMBRE_TABLA = "Ruta";
        private const string COLUMNA_ID = "idRuta";
        public string IdRutaMax {  get; set; }
        public string IdRutaMin { get; set; }

        private readonly int _columna = 0;

        private readonly string _query;
        private string _resultadoQuery;
        public Ruta()
        {
            _query = $"SELECT MIN({COLUMNA_ID}) " +
                $"FROM {NOMBRE_TABLA} AS r " +
                $"inner join preventista as p " +
                $"on p.idpreventa = r.idpreventa " +
                $"nwhere p.inactivo = 0";

            Datos(_query);
            IdRutaMin = _resultadoQuery;

            _query = $"SELECT MAX({COLUMNA_ID}) " +
                $"FROM {NOMBRE_TABLA} AS r " +
                $"inner join preventista as p " +
                $"on p.idpreventa = r.idpreventa " +
                $"nwhere p.inactivo = 0";

            Datos( _query );
            IdRutaMax = _resultadoQuery;
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
