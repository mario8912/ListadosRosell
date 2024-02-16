using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Datos;
using Tablas;

namespace Entidades
{
    public static class Global
    {
        public static string CadenaConexion;
        public static string RutaAplicacion = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string RutaDirectorioInformes = TryRutaInformes();
        public static string RutaReporte;
        public static ReportDocument ReporteCargado;
        private static string TryRutaInformes()
        {
            string dirInformes = Path.Combine(RutaAplicacion, "Informes");

            if (Directory.Exists(dirInformes)) return dirInformes;
            else throw new DirectoryNotFoundException("El directorio Informes no existe");
        }

        public static void AgregarHijoMDI(Form frmPadre, Form frmHijo)
        {
            frmHijo.MdiParent = frmPadre;
        }
        
        public const string MONEDA= "EURO";
        public const float VALOR_MONEDA = 166.386f;

        public const string subfamiliaIni = " ";
        public const string subfamiliaFin = "ZZZZ";

        public static string familiaIni;
        public static string familiaFin;

        public static string articuloIni;
        public static string articuloFin;
        
        public static bool inactivo = true;

        public static DateTime fechaIni;
        public static DateTime fechaFin;
    }
}