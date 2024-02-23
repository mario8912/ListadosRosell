using System;

namespace Capas.FormularioReporte
{
    public static class CondicionesParametros
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
    }
}
