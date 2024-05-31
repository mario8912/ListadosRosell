using Presentacion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Entidades.Global;
using System.Threading.Tasks;

namespace UnitTestProject.Tests
{
    public class RutaInformes
    {
        public RutaInformes()
        {
            Console.WriteLine("MDI_Principal Tests:");
        }

        public async void TryRutaInformes_RutaCorrecta()
        {
            // Arrange
            ProgramConfig programConfig = new ProgramConfig();

            string expectedResult = @"D:\miPc\desktop\ListadosRosell\Presentacion\bin\Debug\InformesCRP";
            string result;
            // Act
            Task task = Task.Run(() => programConfig.TryRutaInformes());
            await task;
            
            result = GlobalInformes.RutaDirectorioInformes;

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
