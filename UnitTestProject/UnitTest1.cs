using Capas;
using Entidades.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private readonly TestServiceProvider _testServiceProvider;
        private readonly GlobalInformes _globalInfomes;

        public UnitTest1()
        {
            _testServiceProvider = new TestServiceProvider();
            _globalInfomes = _testServiceProvider.ServiceProvider.GetRequiredService<GlobalInformes>();
        }

<<<<<<< HEAD
        /*[TestMethod]
        public void MDI_Principal()
        {
            MDI_PrincipalTests mdiPrincipalTest = new MDI_PrincipalTests(_globalInfomes);
            mdiPrincipalTest.TryGlobaDirectorioInfomes_True();
        }*/

        
        public void BusquedaRecursiva()
        {
            BusquedaRecursivaTest brTest = new BusquedaRecursivaTest();
            brTest.BuscquedaRecursiva_RutaDirectorioInformes();
=======
        [TestMethod]
        public void MDI_PrincipalTest()
        {
            MDI_PrincipalTests mdiPrincipalTest = new MDI_PrincipalTests(_globalInfomes);
            mdiPrincipalTest.TryGlobaDirectorioInfomes_True();
>>>>>>> aa6fa2d (unit and services)
        }
    }
}
