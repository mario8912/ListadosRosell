using Datos;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Entidades.Modelos
{
    public static class ConsultaParametros
    {
        private static string _parametro;
        private static bool _minMax;
        
       public static string ConsultaParametro(string parametro, bool minMax)
       {
            _parametro = parametro;
            _minMax = minMax;

            Stopwatch sp = new Stopwatch();
            sp.Start();

            using (Conexion cn = new Conexion())
            {
                cn.AbrirConexion();

                var consulta = SwitchParametro();

                if(consulta != string.Empty)
                {
                    using (SqlCommand commando = new SqlCommand(consulta, cn._conexionSql))
                    {
                        SqlDataReader reader = commando.ExecuteReader();
                        reader.Read();
                        
                        var respuesta = reader.GetValue(0).ToString();

                        reader.Close();
                        cn.Dispose();

                        return respuesta;
                    }
                }
                cn.Dispose();
                sp.Stop();
                Console.WriteLine(sp.Elapsed);
                return "";
            }
       }

        private static string SwitchParametro() 
        {
            string minMaxQuery = _minMax ? "MIN" : "MAX";

            switch(_parametro.ToLower())
            {
                case "idcliente":
                case "cliente":
                    return $"" +
                        $"SELECT {minMaxQuery}(idCliente)" +
                        $" FROM cliente" +
                        $" WHERE eliminado = 0";

                case "idtipocliente":
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
                        $" FROM ruta AS r " +
                        $" INNER JOIN preventista AS p " +
                        $" ON p.idpreventa = r.idpreventa" +
                        $" WHERE p.inactivo = 0" +
                        $" AND r.idruta < 1000";

                case "articulo":
                    return $"" +
                        $"SELECT {minMaxQuery}(idArticulo)" +
                        $" FROM articulo" +
                        $"W HERE eliminado = 0";

                case "familia":
                    return $"" +
                        $" SELECT {minMaxQuery}(idFamilia)" +
                        $" FROM familia";

                case "proveedor":
                    return $"" +
                        $" SELECT {minMaxQuery}(idProveedor)" +
                        $" FROM proveedor";

                default:
                    return string.Empty; 
            }
        }
    }
}
