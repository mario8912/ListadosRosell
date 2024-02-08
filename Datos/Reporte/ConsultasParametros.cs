namespace Datos
{
    public class ConsultasParametros : Conexion
    {
        private readonly string _tabla;
        private readonly string _columna;
        private readonly bool _minimoMaximo; //true max, false min    
        public string ResultadoQuery { get; set; }
        public ConsultasParametros(string tabla, string columna, bool minimoMaximo) 
        {
            _tabla = tabla;
            _columna = columna;
            _minimoMaximo = minimoMaximo;
            ResultadoQuery = SqlConexionQuery(GenerarQuery());
        }

        private string GenerarQuery()
        {
            string minMaxQuery = _minimoMaximo ? "MAX" : "MIN";

            return string.Format("SELECT {0}({1}) FROM {2}", minMaxQuery, _columna, _tabla);
        }


    }
}
