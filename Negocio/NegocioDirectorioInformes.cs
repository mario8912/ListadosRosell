using Entidades;
using System.Collections.Generic;
using System.IO;

namespace Negocio
{
    public static class NegocioRutaDirectorioInformes
    {
        public static Dictionary<string, string> DiccionarioSubdirectoriosInformes()
        {
            Dictionary<string, string> claveValorNombreRuta = new Dictionary<string, string>();

            return LlenarDiccionario(claveValorNombreRuta);
        }

        private static Dictionary<string, string> LlenarDiccionario(Dictionary<string, string> claveValorNombreRuta)
        {
            string[] rutaDirectorios = Directory.GetDirectories(GlobalInformes.RutaDirectorioInformes);

            foreach (string directorio in rutaDirectorios)
            {
                claveValorNombreRuta.Add(Path.GetFileName(directorio).ToUpper(), directorio);
            }

            return claveValorNombreRuta;
        }
    }
}
