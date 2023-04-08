using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;



namespace Pruebas
{
    public class Testeo
    {
        protected IWebDriver driver;
        protected ExtentReports extent;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(@"C:\TestResults\ExtentReport.html");
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void BeforeEachTest()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void AfterEachTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : $"<pre>{TestContext.CurrentContext.Result.StackTrace}</pre>";

            switch (status)
            {
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    test.Log(Status.Fail, $"La prueba falló:<br/>{stackTrace}");
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Passed:
                    test.Log(Status.Pass, "La prueba fue realizada con exito!");
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Skipped:
                    test.Log(Status.Skip, "La prueba ha sido omitida");
                    break;
                default:
                    test.Log(Status.Warning, "Estado de la prueba no es reconocido");
                    break;
            }
            driver.Quit();
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            extent.Flush();
        }
    }

    [TestFixture]
    public class GooglePruebas : Testeo
    {
        [Test]
        public void TestBotonBusqueda()
        {
            int TiempoEspera = 2000;
            By BarraBusqueda = By.Name("q");
            By BotonBusqueda = By.Name("btnK");
            By GoogleResultadoTexto = By.XPath(".//h2//span[text()='Agua']");

            Thread.Sleep(TiempoEspera);

            driver.Navigate().GoToUrl("https://www.google.com");

            Thread.Sleep(TiempoEspera);

            driver.FindElement(BarraBusqueda).SendKeys("Agua");

            Thread.Sleep(TiempoEspera);

            driver.FindElement(BotonBusqueda).Click();

            var ResultadoTexto = driver.FindElement(GoogleResultadoTexto);
            Assert.IsTrue(ResultadoTexto.Text.Equals("Agua"));
        }

        [Test]
        public void TestBotonSuerte()
        {
          int TiempoEspera = 1000;

            By BarraBusqueda = By.Name("q");
            By BotonSuerte = By.Name("btnI");

            Thread.Sleep(TiempoEspera);

            driver.Navigate().GoToUrl("https://www.google.com");

            Thread.Sleep(TiempoEspera);

            driver.FindElement(BarraBusqueda).SendKeys("Agua");

            Thread.Sleep(TiempoEspera);

            driver.FindElement(BotonSuerte).Click();

            Thread.Sleep(TiempoEspera);

            Assert.IsTrue(driver.Url.Contains("https://es.wikipedia.org/wiki/Agua"));
        }

        [Test]
        public void TestImagenBoton()
        {
            int TiempoEspera = 2000;

            By ImagenesButton = By.XPath("//a[contains(@href, 'www.google.com.do/imghp?hl=es-419&ogb')]");

            driver.Navigate().GoToUrl("https://www.google.com");

            Thread.Sleep(TiempoEspera);

            driver.FindElement(ImagenesButton).Click();

            Assert.IsTrue(driver.Url.Contains("www.google.com.do/imghp?hl=es-419&ogb"));

        }

        [Test]
        public void TestGoogleBotonGmail()
        {
            int TiempoEspera = 2000;

            By GmailBoton = By.XPath("//a[contains(@href, 'mail.google.com')]");

            Thread.Sleep(TiempoEspera);

            driver.Navigate().GoToUrl("https://www.google.com");

            driver.FindElement(GmailBoton).Click();

            Thread.Sleep(TiempoEspera);

        }


        [Test]
        public void TestImagenBusqueda()
        {
            int TiempoEspera = 2000;
            By BarraBusqueda = By.Name("q");
            By BotonImagen = By.XPath("//a[text()='Imágenes']");

            Thread.Sleep(TiempoEspera);

            driver.Navigate().GoToUrl("https://www.google.com");

            driver.FindElement(BotonImagen).Click();

            Thread.Sleep(TiempoEspera);

            driver.FindElement(BarraBusqueda).SendKeys("Agua");

            Thread.Sleep(TiempoEspera);

            driver.FindElement(BarraBusqueda).Submit();

            Thread.Sleep(TiempoEspera);

            Assert.AreEqual("agua mojada - Buscar con Google", driver.Title);

        }

    }
}