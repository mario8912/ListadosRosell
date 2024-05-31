using Entidades.Global;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Presentacion
{
    public class ProgramConfig
    {
        private static IEnumerable<string> _oddsList;
        private static IEnumerable<string> _evensList;
        private static bool Itemfound = false;

        private static readonly object _lock = new object();

        internal static async void TryRutaInformes()
        {
            SetLists();

            Task searchList = BuscarEnDirectoriosAsync(_oddsList);
            Task searchList1 = BuscarEnDirectoriosAsync(_evensList);

            await Task.WhenAll(searchList, searchList1);

            if (!Directory.Exists(GlobalInformes.RutaDirectorioInformes))
                throw new DirectoryNotFoundException("El directorio Informes no existe. Ruta: " + GlobalInformes.RutaDirectorioInformes);
        }

        private static void SetLists()
        {
            var list = Directory.EnumerateDirectories(GlobalInformes.RutaCarpetaPrincipal).ToList();
            ILookup<bool, string> lookup = list.ToLookup(num => list.IndexOf(num) % 2 == 0);

            _evensList = lookup[true];
            _oddsList = lookup[false];
        }

        static async Task BuscarEnDirectoriosAsync(IEnumerable<string> directorios)
        {
            foreach (var directorio in directorios)
            {
                if (!Itemfound)
                    await Task.Run(() => BusquedaRecursiva(directorio));
                else
                    break;
            }
        }

        private static void BusquedaRecursiva(string ruta)
        {
            var directorios = Directory.EnumerateDirectories(ruta);
            foreach (var subDir in directorios)
            {
                if (subDir.Contains("InformesCRP"))
                {
                    lock (_lock)
                        GlobalInformes.RutaDirectorioInformes = subDir;

                    Itemfound = true;
                    break;
                }

                BusquedaRecursiva(subDir);
            }

            return;
        }
    }
}
