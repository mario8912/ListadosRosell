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
        public static string[] CORREO = { "SINCORRREO", "CONCORREO", "TODOS" };

        public const string subfamiliaIni = " ";
        public const string subfamiliaFin = "ZZZZ";

        public static string familiaIni;
        public static string familiaFin;

        public static string articuloIni;
        public static string articuloFin;
        
        public static bool inactivo = true;

        public static string clienteIni;
        public static string clienteFin;

        public static string rutaIni;
        public static string rutaFin;

        public static string preventaIni;
        public static string preventaFin;

        public static string tipoClienteIni;
        public static string tipoClienteFin;

        public static DateTime fechaIni;
        public static DateTime fechaFin;

        public static int valorMinimoBeneficio;
        public static string idTipoCliente;

        public static async Task ConexionConsulta()
        {
            using (Conexion cn = new Conexion())
            {
                cn.AbrirConexion();

                using (SqlCommand commando = new SqlCommand("", cn._conexionSql))
                {
                    SqlDataReader dataReader = commando.ExecuteReader();
                    
                }
            }
        }
        #region terror
        public static void Asignaciones()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            Task[] tasks = {
                Task.Run(() => Familia()),
                Task.Run(() => Articulo()),
                Task.Run(() => Cliente()),
                Task.Run(() => Ruta()),
                Task.Run(() => Preventa()),
                Task.Run(() => TipoCliente())
            }; 
            
            Task.WaitAll(tasks);
            sp.Stop();
            Console.WriteLine(sp.Elapsed);
        }

        private static void Familia()
        {
            Familia fm = new Familia();
            familiaIni = fm.IdFamiliaMin;
            familiaFin = fm.IdFamiliaMax;
        }

        private static void Articulo()
        {
            Articulo art = new Articulo();
            articuloIni = art.IdArticuloMin;
            articuloFin = art.IdArticuloMax;
        }

        private static void Cliente() 
        {
            Cliente cl = new Cliente();
            clienteIni = cl.IdClienteMin;
            clienteFin = cl.IdClienteMax;
        }

        private static void Ruta()
        {
            Ruta rt = new Ruta();
            rutaIni = rt.IdRutaMin;
            rutaFin = rt.IdRutaMax;
        }

        private static void Preventa()
        {
            Preventa prv = new Preventa();
            preventaIni = prv.IdPreventaMin;
            preventaFin = prv.IdPreventaMax;
        }

        private static void TipoCliente()
        {
            TipoCliente tpCl = new TipoCliente();
            tipoClienteIni = tpCl.IdTipoClienteMin;
            tipoClienteFin = tpCl.IdTipoClienteMax;
        }

#endregion
    }
}

