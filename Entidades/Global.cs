using System;
using System.IO;
using System.Runtime.InteropServices;
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

        public static int clienteIni;
        public static int clienteFin;

        public static int rutaIni;
        public static int rutaFin;

        public static int preventaIni;
        public static int preventaFin;
    }
}
