using Entidades.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Tests
{
    [TestClass]
    internal class BusquedaRecursivaTest
    {
        [TestMethod]
        public void BuscquedaRecursiva_RutaDirectorioInformes()
        {
            //Arrange 
            GlobalInformes globalInformes = new GlobalInformes();
            string expectedResult = @"D:\miPc\desktop\ListadosRosell\Presentacion\bin\Debug\Informes";

            //Act
            _ = globalInformes.BusquedaRecursiva(@"D:\miPc\desktop\ListadosRosell");

            Assert.Equals(expectedResult, globalInformes.RutaDirectorioInformes);
        }
    }
}
