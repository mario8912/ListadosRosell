using Entidades.Global;
using System.Collections.Generic;
using System.IO;

namespace Negocio.Informes
{
    public class NegocioRutaDirectorioInformes
    {
        //DI
        private readonly GlobalInformes _globalInformes;

        public NegocioRutaDirectorioInformes(GlobalInformes globalInformes)
        {
            _globalInformes = globalInformes;
        }

        public Dictionary<string, string> DiccionarioSubdirectoriosInformes()
        {
            Dictionary<string, string> claveValorNombreRuta = new Dictionary<string, string>();

            return LlenarDiccionario(claveValorNombreRuta);
        }

        private Dictionary<string, string> LlenarDiccionario(Dictionary<string, string> claveValorNombreRuta)
        {
            string[] rutaDirectorios = Directory.GetDirectories(_globalInformes.RutaDirectorioInformes);

            foreach (string directorio in rutaDirectorios)
                claveValorNombreRuta.Add(Path.GetFileName(directorio).ToUpper(), directorio);

            return claveValorNombreRuta;
        }
    }
}
