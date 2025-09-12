using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using ExampleLogin.Reporting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ExampleLoginReports.Tests
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private ExtentTest _test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Inicializa el reporte y carpetas
            _ = ExtentReport.Instance;
        }

        [SetUp]
        public void Setup()
        {
            _test = ExtentReport.Instance.CreateTest(TestContext.CurrentContext.Test.Name)
                                          .AssignCategory("Login");

            var options = new ChromeOptions();
            // options.AddArgument("--headless=new");
            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _test.Info("Arranca ChromeDriver y WebDriverWait(10s).");
        }

        [Test]
        public void Login_Ejemplo_SinPOM()
        {
            try
            {
                // 1) Navegar
                _driver.Navigate().GoToUrl("https://testcore.sigesonline.com/");
                _test.Info("Ir a https://testcore.sigesonline.com/");

                // 2) Login
                var usernameField = _wait.Until(d => d.FindElement(By.Id("Email")));
                var passwordField = _driver.FindElement(By.Id("Password"));
                var loginButton = _driver.FindElement(By.XPath("//button[contains(text(),'Iniciar')]"));

                usernameField.SendKeys("admin@plazafer.com");
                _test.Info("Usuario ingresado");

                passwordField.SendKeys("calidad");
                _test.Info("Password ingresado");

                loginButton.Click();
                _test.Info("Click en Iniciar");

                // 3) Aceptar modal si aparece
                try
                {
                    var acceptButton = _wait.Until(d => d.FindElement(By.XPath("//button[contains(text(),'Aceptar')]")));
                    acceptButton.Click();
                    _test.Info("Se aceptó el modal");
                }
                catch (WebDriverTimeoutException)
                {
                    _test.Info("No apareció modal de Aceptar (continuar)");
                }

                // 4) Validar éxito (logo)
                var successElement = _wait.Until(d => d.FindElement(By.XPath("//img[@id='ImagenLogo']")));
                Assert.IsNotNull(successElement, "No se encontró el logo tras el login.");

                _test.Pass("Login OK: se encontró el logo.");
            }
            catch (Exception ex)
            {
                // Logueo; el screenshot se adjunta en TearDown
                _test.Fail("Excepción en el flujo: " + ex.Message);
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var error = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string path = TakeScreenshot(TestContext.CurrentContext.Test.Name);
                if (!string.IsNullOrEmpty(path))
                {
                    var rel = MakeRelative(path, ExtentReport.RunFolder);
                    _test.Fail("La prueba falló: " + error)
                         .AddScreenCaptureFromPath(rel);

                    TestContext.AddTestAttachment(path, "Screenshot de falla");
                }
                else
                {
                    _test.Fail("La prueba falló: " + error + " (sin screenshot)");
                }
            }
            else
            {
                _test.Pass("Prueba OK");
            }

            _driver.Quit();
            _driver.Dispose();

            ExtentReport.Flush();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReport.Flush();
            TestContext.WriteLine($"Reporte en: {ExtentReport.RunFolder}");
        }

        // Helpers mínimos
        private string TakeScreenshot(string testName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

                var safeName = Sanitize(testName);
                var fileName = $"{safeName}_{DateTime.Now:HH-mm-ss}.png";
                var filePath = Path.Combine(ExtentReport.ScreenshotsFolder, fileName);

                var bytes = screenshot.AsByteArray;
                File.WriteAllBytes(filePath, bytes);
                return filePath;
            }
            catch (Exception e)
            {
                _test.Warning("No se pudo tomar screenshot: " + e.Message);
                return string.Empty;
            }
        }

        private static string MakeRelative(string absPath, string baseFolder)
        {
            var uriBase = new Uri(baseFolder + Path.DirectorySeparatorChar);
            var uriFile = new Uri(absPath);
            return Uri.UnescapeDataString(uriBase.MakeRelativeUri(uriFile).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        private static string Sanitize(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}
