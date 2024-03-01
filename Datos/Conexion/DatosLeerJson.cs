using System;
using System.IO;
using System.Text.Json;

namespace Datos.Conexion
{
    internal static class DatosLeerJson
    {
        private static readonly string rutaApp = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string rutaJson = Path.Combine(rutaApp, "conexionCapas.json");

        internal static Configuracion DatosConexionJson()
        {
            string jsonString = File.ReadAllText(rutaJson);

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };

            return JsonSerializer.Deserialize<Configuracion>(jsonString, opciones);
        }
    }

    internal class Configuracion
    {
        public string Servidor { get; set; }
        public string BaseDeDatos { get; set; }
        public SeguridadConfig Seguridad { get; set; }
    }

    internal class SeguridadConfig
    {
        public string Usuario { get; set; }
        public string Contrasenya { get; set; }
        public bool TrustedConnection { get; set; }
    }
}
