using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;


[assembly: InternalsVisibleTo("Capas")]
namespace Entidades.Global
{
    public class GlobalInformes
    {
        //Global informes
        private readonly string RutaExe = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string RutaCarpetaPrincipal;

        CancellationTokenSource _cancellationTokenSource;
        CancellationToken _cancellationToken;

        private StreamWriter _sr;

        public string RutaDirectorioInformes { get; private set; }

        //Global reporte
        public string RutaReporte { get; set; }
        public ReportDocument ReporteCargado { get; set; }

        //Global conexion
        public string CadenaConexion { get; set; }

        public GlobalInformes()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            RutaCarpetaPrincipal = Path.GetFullPath(Path.Combine(RutaExe, @"..\..\..\"));
            TryRutaInformes();

            FileStream fs = new FileStream(Path.GetFullPath(Path.Combine(RutaCarpetaPrincipal, @"..\log.txt")), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            _sr = new StreamWriter(fs);
        }

        private string TryRutaInformes()
        {
            /*Task tarea = Task.Run(() =>
            {
                BusquedaRecursiva(RutaCarpetaPrincipal);
            }, _cancellationToken);

            try
            {
                if (!tarea.Wait(TimeSpan.FromMilliseconds(100000)))
                {
                    MessageBox.Show("bye");
                    _cancellationTokenSource.Cancel();
                }
            }
            catch (Exception)
            {
            }
            */

            BusquedaRecursiva(RutaCarpetaPrincipal);

            if (Directory.Exists(RutaDirectorioInformes))
                return RutaDirectorioInformes;
            else
                throw new DirectoryNotFoundException("El directorio Informes no existe. Ruta: " + RutaDirectorioInformes);
        }

        public List<string> BusquedaRecursiva(string ruta)
        {
            var list = Directory.EnumerateDirectories(ruta).ToList();
            var lookup = list.ToLookup(num => list.IndexOf(num) % 2 == 0);

            IEnumerable<string> trueList = lookup[true];
            IEnumerable<string> falseList = lookup[false];

            Foo(trueList);
            Foo(falseList);

            return list;
        }

        private bool Foo(IEnumerable<string> list)
        {
            List<string> files = new List<string>();

            foreach (var subDir in list)
            {
                if (!subDir.Equals(@"D:\miPc\desktop\ListadosRosell\.git"))
                {
                    if (subDir.Contains("Informes"))
                    {
                        RutaDirectorioInformes = subDir;
                        return true;
                    }

                    Console.WriteLine(subDir);
                    files.AddRange(BusquedaRecursiva(subDir));
                }
            }

            return false;
        }
    }
}