using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

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
            string[] rutaDirectorios = Directory.GetDirectories(Global.RutaDirectorioInformes);

            foreach (string directorio in rutaDirectorios){
                claveValorNombreRuta.Add(Path.GetFileName(directorio).ToUpper(), directorio);
            }
            
            return claveValorNombreRuta;
        }
    }
}
