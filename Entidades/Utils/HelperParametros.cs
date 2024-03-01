using System;

namespace Entidades.Utils
{
    public static class HelperParametros
    {
        public static readonly Func<string, bool> DiferenteDeINI = (iniFinParam) => iniFinParam != "INI";
        public static readonly Func<string, bool> DiferenteDeFIN = (iniFinParam) => iniFinParam != "FIN";
        public static readonly Func<string, bool> IgualA_INI = (iniFinParam) => iniFinParam == "INI";
        public static readonly Func<string, bool> IgualA_FIN = (iniFinParam) => iniFinParam == "FIN";

        public static readonly Func<string, bool> DiferenteDeDESDE = (desdeHastaParam) => desdeHastaParam != "DESDE";
        public static readonly Func<string, bool> DiferenteDeHASTA = (desdeHastaParam) => desdeHastaParam != "HASTA";
        public static readonly Func<string, bool> IgualA_DESDE = (desdeHastaParam) => desdeHastaParam == "DESDE";
        public static readonly Func<string, bool> IgualA_HASTA = (desdeHastaParam) => desdeHastaParam == "HASTA";

        public static readonly Func<string, bool> DiferenteDeINI_O_FIN = (iniFinParam) => DiferenteDeFIN(iniFinParam) || DiferenteDeINI(iniFinParam);
        public static readonly Func<string, bool> IgualA_INI_O_FIN = (iniFinParam) => IgualA_INI(iniFinParam) || IgualA_FIN(iniFinParam);

        public static readonly Func<string, bool> DiferenteDeDESDE_O_HASTA = (desdeHastaParam) => DiferenteDeDESDE(desdeHastaParam) || DiferenteDeHASTA(desdeHastaParam);
        public static readonly Func<string, bool> IgualA_DESDE_O_HASTA = (desdeHastaParam) => IgualA_DESDE(desdeHastaParam) || IgualA_HASTA(desdeHastaParam);

        public static readonly Func<string, string, bool> DiferenteDeTodo = (iniFinParam, desdeHastaParam) => DiferenteDeDESDE_O_HASTA(iniFinParam) || DiferenteDeINI_O_FIN(desdeHastaParam);
        public static readonly Func<string, string, bool> IgualA_Todo = (iniFinParam, desdeHastaParam) => IgualA_INI_O_FIN(iniFinParam) || IgualA_DESDE_O_HASTA(desdeHastaParam);

        public static string SwitchConsultaParametro(string parametro, bool minMax)
        {
            string minMaxQuery = minMax ? "MIN" : "MAX";

            switch (parametro)
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
        public static string SwitchSiConsultaVacia(string nombreParametro,  bool minMax)
        {
            var respuesta = "";
            switch (nombreParametro)
            {
                case "dia":
                    if (minMax) respuesta = "1";
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
    }
}
