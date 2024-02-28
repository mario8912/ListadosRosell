using Datos.Conexiones;
using System.Data.SqlClient;

namespace Entidades.Modelos
{
    public static class NegocioReporteConsulta
    {
        private static string _parametro;
        private static bool _minMax;
        private static string _consulta;
        
       public static string ConsultaParametro(string parametro, bool minMax)
       {
            _parametro = parametro.ToLower();
            _minMax = minMax;
            _consulta = SwitchParametro();

            var respuesta = "";

            if (_consulta == string.Empty || _consulta == "")
            {
                switch (_parametro)
                {
                    case "dia":
                        if (minMax) respuesta = "0";
                        else respuesta = "5";
                        break;

                    case "anyo":
                        if (minMax) respuesta = "2017";
                        else respuesta = "2024";
                        break;

                    case "mes":
                        if (minMax) respuesta = "enero";
                        else respuesta = "diciembre";
                        break;

                    case "cp":
                        respuesta = "12001";
                        break;

                    default:
                        break;
                }
                return respuesta;
            }
            else
            {
                using (Conexion cn = new Conexion())
                {
                    cn.AbrirConexion();
                    
                    using (SqlCommand commando = new SqlCommand(_consulta, cn._conexionSql))
                    {
                        SqlDataReader reader = commando.ExecuteReader();
                        reader.Read();
                        respuesta = reader.GetValue(0).ToString();

                        reader.Close();
                        cn.Dispose();

                        return respuesta;
                    }
                }
            }
       }

        private static string SwitchParametro() 
        {
            string minMaxQuery = _minMax ? "MIN" : "MAX";

            switch(_parametro)
            {
                case "idcliente":
                case "cliente":
                    return $"" +
                        $"SELECT {minMaxQuery}(idCliente)" +
                        $" FROM cliente" +
                        $" WHERE eliminado = 0";

                case "idtipocliente":
                case "tipo cliente":
                case "tipocliente":
                    return $"SELECT {minMaxQuery}(idTipoCliente)" +
                        $" FROM tipocliente";

                case "preventa":
                case "preventista":
                    return $"" +
                        $"SELECT {minMaxQuery}(idPreventa)" +
                        $" FROM preventista" +
                        $" WHERE inactivo = 0";

                case "ruta":
                    return $"" +
                        $" SELECT {minMaxQuery}(idRuta)" +
                        $" FROM ruta " +
                        $" WHERE 1 = 1 " +
                        $" AND idruta < 90";

                case "articulo":
                    return $"" +
                        $"SELECT {minMaxQuery}(idArticulo)" +
                        $" FROM articulo" +
                        $" WHERE eliminado = 0";

                case "familia":
                    return $"" +
                        $" SELECT {minMaxQuery}(idFamilia)" +
                        $" FROM familia";

                case "subfamilia":
                    return $"" +
                        $" SELECT {minMaxQuery}(subfamilia)" +
                        $" FROM familia";

                case "proveedor":
                    return $"" +
                        $" SELECT {minMaxQuery}(idProveedor)" +
                        $" FROM proveedor";

                case "repartidor":
                    return $"" +
                        $"SELECT {minMaxQuery}(idRepartidor)" +
                        $" FROM repartidor" +
                        $" WHERE idRepartidor <> 100";

                case "idprovgasto":
                    return $"" +
                        $"SELECT {minMaxQuery}(idprovgasto)" +
                        $" FROM provgasto";

                case "conceptogasto":
                case "concepto":
                    return $"" +
                        $"SELECT {minMaxQuery}(idConceptoGasto)" +
                        $"FROM conceptoGasto";
                default:
                    return string.Empty; 
            }
        }
    }
}
