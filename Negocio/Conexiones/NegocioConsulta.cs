using Datos.Conexiones;
using Entidades.Global;
using Entidades.Utils;

namespace Negocio.Conexiones
{
    public class NegocioConsulta
    {
        //DI
        private readonly GlobalInformes _globalInformes;

        private string _parametro;
        private string _consulta;
        private readonly DatosConexion _datosConexion;

        public NegocioConsulta(GlobalInformes globalInformes)
        {
            _globalInformes = globalInformes;
            _datosConexion = new DatosConexion(_globalInformes);
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
