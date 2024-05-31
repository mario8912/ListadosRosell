using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Capas")]
namespace Entidades.Global
{
    public static class GlobalInformes
    {
        //Global informes
        private static readonly string RutaExe = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string RutaCarpetaPrincipal = Path.GetFullPath(Path.Combine(RutaExe, @"..\..\..\"));

        public static string RutaDirectorioInformes { get; set; } 

        //Global reporte
        public static string RutaReporte { get; set; }
        public static ReportDocument ReporteCargado { get; set; }

        //Global conexion
        public static string CadenaConexion { get; set; }
    }
}