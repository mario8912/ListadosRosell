using Entidades.Global;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class ProgramConfig
    {
        private IEnumerable<string> _oddsList;
        private IEnumerable<string> _evensList;
        private bool Itemfound = false;

        private readonly object _lock = new object();

        internal async Task TryRutaInformes()
        {
            SetLists();

            Task searchList = BuscarEnDirectoriosAsync(_oddsList);
            Task searchList1 = BuscarEnDirectoriosAsync(_evensList);

            await Task.WhenAll(searchList, searchList1);

            if (!Directory.Exists(GlobalInformes.RutaDirectorioInformes))
                throw new DirectoryNotFoundException("El directorio Informes no existe. Ruta: " + GlobalInformes.RutaDirectorioInformes);
        }

        private void SetLists()
        {
            var list = Directory.EnumerateDirectories(GlobalInformes.RutaCarpetaPrincipal).ToList();
            ILookup<bool, string> lookup = list.ToLookup(num => list.IndexOf(num) % 2 == 0);

            _evensList = lookup[true];
            _oddsList = lookup[false];
        }

        async Task BuscarEnDirectoriosAsync(IEnumerable<string> directorios)
        {
            foreach (var directorio in directorios)
            {
                if (!Itemfound)
                    await Task.Run(() => BusquedaRecursiva(directorio));
                else
                    break;
            }
        }

        private void BusquedaRecursiva(string ruta)
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
