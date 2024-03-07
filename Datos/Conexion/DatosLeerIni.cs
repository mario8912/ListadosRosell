using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

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

        [TestFixture]
        public class DatosConexionIniTests
        {
            private DatosConexionIni _datosConexionIni;

            [SetUp]
            public void SetUp()
            {
                _datosConexionIni = new DatosConexionIni();
            }

            [Test]
            public void TestServiorProperty()
            {
                string expected = "TestServer";
                _datosConexionIni.Servior = expected;
                Assert.AreEqual(expected, _datosConexionIni.Servior);
            }

            [Test]
            public void TestBaseDeDatosProperty()
            {
                string expected = "TestDatabase";
                _datosConexionIni.BaseDeDatos = expected;
                Assert.AreEqual(expected, _datosConexionIni.BaseDeDatos);
            }

            [Test]
            public void TestTrustedConnectionProperty()
            {
                bool expected = true;
                _datosConexionIni.TrustedConnection = expected;
                Assert.AreEqual(expected, _datosConexionIni.TrustedConnection);
            }

            [Test]
            public void TestUsuarioProperty()
            {
                string expected = "TestUser";
                _datosConexionIni.Usuario = expected;
                Assert.AreEqual(expected, _datosConexionIni.Usuario);
            }

            [Test]
            public void TestContraseniaProperty()
            {
                string expected = "TestPassword";
                _datosConexionIni.Contrasenia = expected;
                Assert.AreEqual(expected, _datosConexionIni.Contrasenia);
            }
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


