using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class DatosRutaInformes
    {
        private const string DirectorioInformes = "Informes";
        public static string ObtenerRutaCompletaInformes()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DirectorioInformes);
        }
    }
}

