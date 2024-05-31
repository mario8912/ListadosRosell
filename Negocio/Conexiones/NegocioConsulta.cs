using Datos.Conexiones;
using Entidades.Global;
using Entidades.Utils;

namespace Negocio.Conexiones
{
    public class NegocioConsulta
    {
        private string _parametro;
        private string _consulta;
        private readonly DatosConexion _datosConexion;

        public NegocioConsulta()
        {
            _datosConexion = new DatosConexion();
        }

        public string ConsultaParametro(string parametro, bool minMax)
        {
            _parametro = parametro.ToLower();
            _consulta = HelperParametros.SwitchConsultaParametro(_parametro, minMax);

            if (_consulta == string.Empty || _consulta == "")
                return HelperParametros.SwitchSiConsultaVacia(_parametro, minMax);
            else
                return _datosConexion.EjecutarConsulta(_consulta);
        }
    }
}
