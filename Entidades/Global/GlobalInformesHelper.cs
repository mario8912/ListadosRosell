using System;
using System.IO;

namespace Entidades.Global
{
    internal static class GlobalInformesHelper
    {
        private static string RutaAplicacion = AppDomain.CurrentDomain.BaseDirectory;
        internal static string TryRutaInformes()
        {
            string dirInformes = Path.Combine(RutaAplicacion, "Informes__");

            if (Directory.Exists(dirInformes)) return dirInformes;
            else throw new DirectoryNotFoundException("El directorio Informes no existe");
        }
    }
}
