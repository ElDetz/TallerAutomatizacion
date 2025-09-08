using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Support.UI;

namespace ExampleLogin
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            // Inicializar el WebDriver antes de cada prueba
            _driver = new ChromeDriver();
        }

        [Test]
        public void TestExampleSite()
        {
            // Navegar a la URL
            _driver.Navigate().GoToUrl("https://testcore.sigesonline.com/");

            Thread.Sleep(4000);

            // Encontrar los elementos de login
            var usernameField = _driver.FindElement(By.XPath("//input[@id='Email']"));
            var passwordField = _driver.FindElement(By.XPath("//input[@id='Password']"));
            var loginButton = _driver.FindElement(By.XPath("//button[contains(text(),'Iniciar')]"));

            usernameField.SendKeys("admin@plazafer.com");
            Thread.Sleep(2000);
            passwordField.SendKeys("calidad");
            Thread.Sleep(2000);
            loginButton.Click(); // Ingresar
            Thread.Sleep(4000);

            // Aceptar
            var acceptButton = _driver.FindElement(By.XPath("//button[contains(text(),'Aceptar')]"));
            acceptButton.Click();
            Thread.Sleep(4000);

            // Validar el éxito del login
            var successElement = _driver.FindElement(By.XPath("//img[@id='ImagenLogo']"));
            Assert.IsNotNull(successElement, "No se encontró el elemento de éxito después del login.");
        }

        [TearDown]
        public void TearDown()
        {
            // Cerrar el navegador después de cada prueba
            _driver.Quit();
            _driver.Dispose(); // Libera memoria y recursos no administrados
        }
    }
}
