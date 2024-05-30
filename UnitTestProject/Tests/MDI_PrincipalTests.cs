using Capas;
using Entidades.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace UnitTestProject.Tests
{
    [TestClass]
    public class MDI_PrincipalTests
    {
        private readonly GlobalInformes _globalInformes;

        public MDI_PrincipalTests(GlobalInformes globalInformes)
        {
            _globalInformes = globalInformes;
            Console.WriteLine("MDI_Principal Tests:");
        }

        [TestMethod]
        public void TryGlobaDirectorioInfomes_True()
        {
            // Arrange
            MDI_Principal mDI_Principal = new MDI_Principal(_globalInformes);

            // Act
            bool result = mDI_Principal.TryGlobaDirectorioInfomes();

            // Assert
            Assert.IsTrue(result);
        }
    }
}
