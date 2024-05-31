using Entidades.Global;
using System.Collections.Generic;
using System.IO;

namespace Negocio.Informes
{
    public class NegocioRutaDirectorioInformes
    {
        public Dictionary<string, string> DiccionarioSubdirectoriosInformes()
        {
            Dictionary<string, string> claveValorNombreRuta = new Dictionary<string, string>();

            return LlenarDiccionario(claveValorNombreRuta);
        }

        private Dictionary<string, string> LlenarDiccionario(Dictionary<string, string> claveValorNombreRuta)
        {
            string[] rutaDirectorios = Directory.GetDirectories(GlobalInformes.RutaDirectorioInformes);

            foreach (string directorio in rutaDirectorios)
                claveValorNombreRuta.Add(Path.GetFileName(directorio).ToUpper(), directorio);

            return claveValorNombreRuta;
        }
    }
}
