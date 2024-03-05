using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Datos.Conexion
{
    internal class DatosLeerIni
    {
        private static readonly string rutaApp = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string rutaIni = Path.Combine(rutaApp, "conexionCapas.ini");
        private static IConfiguration _ini;

        internal static DatosConexionIni AsignarDatosIni()
        {
            _ini = new ConfigurationBuilder().AddIniFile(rutaIni).Build();
            DatosConexionIni conexionIni = new DatosConexionIni();

            IConfiguration seccionConexion = _ini.GetSection("CONEXION");
            conexionIni.Servior = seccionConexion["servidor"];
            conexionIni.BaseDeDatos = seccionConexion["baseDeDatos"];

            IConfiguration seccionSeguridad = _ini.GetSection("SEGURIDAD");
            conexionIni.TrustedConnection = bool.Parse(seccionSeguridad["trustedConnection"]);
            
            return conexionIni;
        }
    }

    internal class DatosConexionIni
    {
        public string Servior { get; set; }
        public string BaseDeDatos { get; set; }
        public bool TrustedConnection { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
    }
}
