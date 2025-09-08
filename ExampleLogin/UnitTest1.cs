using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;
using System.Threading;

namespace ExampleLogin
{
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            try
            {
                // Inicializa ExtentReports antes de cada prueba
                ExtentReport.ExtentReportInit();
                ExtentReport.StartTest("LoginTest");

                var options = new ChromeOptions();
                driver = new ChromeDriver(options);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                Console.WriteLine("Test setup completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during test setup: " + ex.Message);
            }
        }

        [Test]
        public void LoginCorrecto()
        {
            try
            {
                // Navegar a la URL
                driver.Navigate().GoToUrl("https://testcore.sigesonline.com/");
                Thread.Sleep(4000);

                // Encontrar los elementos de login
                var usernameField = driver.FindElement(By.XPath("//input[@id='Email']"));
                var passwordField = driver.FindElement(By.XPath("//input[@id='Password']"));
                var loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Iniciar')]"));

                usernameField.SendKeys("admin@plazafer.com");
                Thread.Sleep(2000);
                passwordField.SendKeys("calidad");
                Thread.Sleep(2000);
                loginButton.Click();
                Thread.Sleep(4000);

                // Aceptar la alerta
                var acceptButton = driver.FindElement(By.XPath("//button[contains(text(),'Aceptar')]"));
                acceptButton.Click();
                Thread.Sleep(4000);

                // Validar el éxito del login
                var successElement = wait.Until(drv => drv.FindElement(By.XPath("//img[@id='ImagenLogo']")));
                Assert.IsNotNull(successElement, "No se encontró el elemento de éxito después del login.");

                // Si la prueba pasa, se marca como pasada
                ExtentReport._test.Pass("Login test passed!");
            }
            catch (Exception ex)
            {
                // Si la prueba falla, tomar una captura de pantalla y marcar como fallada
                string screenshotPath = ExtentReport.AddScreenshot(driver); // Agregar captura
                ExtentReport._test.Fail("Login test failed! Error: " + ex.Message);
                ExtentReport._test.AddScreenCaptureFromPath(screenshotPath); // Añadir captura al reporte
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                // Finaliza el reporte después de cada prueba
                ExtentReport.ExtentReportTearDown();

                driver.Quit();
                driver.Dispose(); // Libera memoria y recursos no administrados
                Console.WriteLine("Test teardown completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during test teardown: " + ex.Message);
            }
        }
    }
}
