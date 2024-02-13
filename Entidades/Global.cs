using System;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Entidades
{
    public static class Global
    {
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

        /*
        public const string MONEDA= "EURO";
        public const float VALOR_MONEDA = 166.386f;
        public static string[] CORREO = { "SINCORRREO", "CONCORREO", "TODOS" };

        public static string familiaIni = "";
        public static string familiaFin = "ZZZZ";

        public static string subfamiliaIni = "";
        public static string subfamiliaFin = "ZZZZ";

        public static string articuloIni = "";
        public static string articuloFin = "ZZZZ";
        
        public static bool inactivo = true;

        public static int clienteIni;
        public static int clienteFin;

        public static int rutaIni;
        public static int rutaFin;

        public static int preventaIni;
        public static int preventaFin;

        public static DateTime fechaIni;
        public static DateTime fechaFin;

        public static int valorMinimoBeneficio;
        public static string idTipoCliente;
        */
    }
}

