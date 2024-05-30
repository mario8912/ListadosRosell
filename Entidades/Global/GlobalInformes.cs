using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;


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

        public string RutaDirectorioInformes
        {
            get
            {
                return TryRutaInformes();
            }
            private set { }
        }

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
        }

        private string TryRutaInformes()
        {
            Task tarea = Task.Run(() =>
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
            
            if (Directory.Exists(RutaDirectorioInformes))
                return RutaDirectorioInformes;
            else
                throw new DirectoryNotFoundException("El directorio Informes no existe. Ruta: " + RutaDirectorioInformes);
        }

        private List<string> BusquedaRecursiva(string ruta)
        {
            List<string> files = new List<string>();

            foreach (var subDir in Directory.EnumerateDirectories(ruta))
            {
                string dir = subDir.Split('\\').ToList().Last();

                if (subDir.Contains(dir))
                {
                    RutaDirectorioInformes = subDir;
                    break;
                }
                else
                    files.AddRange(BusquedaRecursiva(subDir));
            }

            return files;
        }

        private void Print(string dir)
        {
            var txt = Path.GetFullPath(Path.Combine(RutaCarpetaPrincipal, @"..\log.txt"));
            _sr = new StreamWriter(txt);

            _sr.WriteLine(dir, true);
        }
    }
}