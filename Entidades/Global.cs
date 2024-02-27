using System;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace Entidades
{
    public static class Global
    {                           
        public static string RutaAplicacion = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string RutaDirectorioInformes = TryRutaInformes();

        public static string RutaReporte;
        public static ReportDocument ReporteCargado;

        public static string CadenaConexion;

        public const string MONEDA= "EURO";
        public const float VALOR_MONEDA = 166.386f;

        public const string subfamiliaIni = "ACE";
        public const string subfamiliaFin = "ZZZZ";

        public static string TryRutaInformes()
        {
            string dirInformes = Path.Combine(RutaAplicacion, "Informes__");

            if (Directory.Exists(dirInformes)) return dirInformes;
            else throw new DirectoryNotFoundException("El directorio Informes no existe");
        }
    }
}