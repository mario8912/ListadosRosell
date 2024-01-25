using System;
using System.IO;
using System.Windows.Forms;

namespace Entidades
{
    public static class Global
    {
        public static string RutaAplicacion = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string RutaDirectorioInformes = TryRutaInformes();
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
    }
}
