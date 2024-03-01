using Datos.Conexiones;
using Entidades.Utils;

namespace Negocio.Conexiones
{
    public static class NegocioConsulta
    {
        private static string _parametro;
        private static string _consulta;
        private static readonly IfDatosConexion _ifDatosConexion = new DatosConexion();

        public static string ConsultaParametro(string parametro, bool minMax)
        {
            _parametro = parametro.ToLower();
            _consulta = HelperParametros.SwitchConsultaParametro(_parametro, minMax);

            if (_consulta == string.Empty || _consulta == "")
            {
                return HelperParametros.SwitchSiConsultaVacia(_parametro, minMax);
            }
            else
            {
                return _ifDatosConexion.EjecutarConsulta(_consulta);
            }
        }
    }
}
