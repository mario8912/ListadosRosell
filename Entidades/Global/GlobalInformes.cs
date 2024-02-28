using CrystalDecisions.CrystalReports.Engine;
using Entidades.Global;

namespace Entidades
{
    public static class GlobalInformes
    {
        //Global informes
        public static readonly string RutaDirectorioInformes = GlobalInformesHelper.TryRutaInformes();

        //Global reporte
        public static string RutaReporte;
        public static ReportDocument ReporteCargado;

        //Global conexion
        public static string CadenaConexion;
    }
}