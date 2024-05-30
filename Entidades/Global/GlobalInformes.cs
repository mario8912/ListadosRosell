using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Capas")]
namespace Entidades.Global
{
    public class GlobalInformes
    {
        //Global informes
        private readonly string RutaExe = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string RutaCarpetaPrincipal;

        public string RutaDirectorioInformes
        {
            get
            {
                return TryRutaInformes();
            }
            set {}
        }

        //Global reporte
        public string RutaReporte { get; set; }
        public ReportDocument ReporteCargado { get; set; }

        //Global conexion
        public string CadenaConexion { get; set; }

        public GlobalInformes()
        {
            RutaCarpetaPrincipal = Path.GetFullPath(Path.Combine(RutaExe, @"..\..\..\"));
        }

        private string TryRutaInformes()
        {
            BusquedaRecursiva(RutaCarpetaPrincipal);

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
                if (subDir.Contains("Informes"))
                {
                    RutaDirectorioInformes = subDir;
                    break;
                }
                else
                    files.AddRange(BusquedaRecursiva(subDir));
            }

            return files;
        }
    }
}