using Entidades.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentacion;
using System;
using System.Threading.Tasks;
using UnitTestProject.Tests;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void RutaInformesTest()
        {
            //Arrange
            ProgramConfig programConfig = new ProgramConfig();
            string expectedResult = @"D:\miPc\desktop\ListadosRosell\Presentacion\bin\Debug\InformesCRP";
            string result; 

            // Act
            programConfig.TryRutaInformes().Wait();
            result = GlobalInformes.RutaDirectorioInformes;
            // Assert

            Assert.AreEqual(expectedResult, result);
        }
    }
}
