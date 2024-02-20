using System;

namespace Capas.FormularioReporte
{
    internal static class CondicionesParametros
    {

        internal static readonly Func<string, bool> DiferenteDeINI = (iniFinParam) => iniFinParam != "INI";
        internal static readonly Func<string, bool> DiferenteDeFIN = (iniFinParam) => iniFinParam != "FIN";
        internal static readonly Func<string, bool> IgualA_INI = (iniFinParam) => iniFinParam == "INI";
        internal static readonly Func<string, bool> IgualA_FIN = (iniFinParam) => iniFinParam == "FIN";

        internal static readonly Func<string, bool> DiferenteDeDESDE = (desdeHastaParam) => desdeHastaParam != "DESDE";
        internal static readonly Func<string, bool> DiferenteDeHASTA = (desdeHastaParam) => desdeHastaParam != "HASTA";
        internal static readonly Func<string, bool> IgualA_DESDE = (desdeHastaParam) => desdeHastaParam == "DESDE";
        internal static readonly Func<string, bool> IgualA_HASTA = (desdeHastaParam) => desdeHastaParam == "HASTA";

        internal static readonly Func<string, bool> DiferenteDeINI_O_FIN = (iniFinParam) => DiferenteDeFIN(iniFinParam) || DiferenteDeINI(iniFinParam);
        internal static readonly Func<string, bool> IgualA_INI_O_FIN = (iniFinParam) => IgualA_INI(iniFinParam) || IgualA_FIN(iniFinParam);

        internal static readonly Func<string, bool> DiferenteDeDESDE_O_HASTA = (desdeHastaParam) => DiferenteDeDESDE(desdeHastaParam) || DiferenteDeHASTA(desdeHastaParam);
        internal static readonly Func<string, bool> IgualA_DESDE_O_HASTA = (desdeHastaParam) => IgualA_DESDE(desdeHastaParam) || IgualA_HASTA(desdeHastaParam);

        internal static readonly Func<string, string, bool> DiferenteDeTodo = (iniFinParam, desdeHastaParam) => DiferenteDeDESDE_O_HASTA(iniFinParam) || DiferenteDeINI_O_FIN(desdeHastaParam);
        internal static readonly Func<string, string, bool> IgualA_Todo = (iniFinParam, desdeHastaParam) => IgualA_INI_O_FIN(iniFinParam) || IgualA_DESDE_O_HASTA(desdeHastaParam);
    }
}
