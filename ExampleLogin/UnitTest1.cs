using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ExampleLogin
{
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        // Configuración de prueba
        private const string LoginUrl = "https://testcore.sigesonline.com/";
        private const string UsernameValue = "admin@plazafer.com";
        private const string PasswordValue = "calidad";
        private const string UsernameSelector = "//input[@id='Email']";
        private const string PasswordSelector = "//input[@id='Password']";
        private const string SubmitSelector = "//button[normalize-space()='Iniciar']";
        private const string AcceptSelector = "//button[normalize-space()='Aceptar']";
        private const string SuccessSelector = "//img[@id='ImagenLogo']";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void LoginCorrecto()
        {
            driver.Navigate().GoToUrl("https://testcore.sigesonline.com/");
            Thread.Sleep(4000);

            var usernameField = driver.FindElement(By.XPath("//input[@id='Email']"));
            var passwordField = driver.FindElement(By.XPath("//input[@id='Password']"));
            var loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Iniciar')]"));
                  
            usernameField.SendKeys("admin@plazafer.com");
            Thread.Sleep(2000);

            passwordField.SendKeys("calidad");
            Thread.Sleep(2000);

            loginButton.Click();
            Thread.Sleep(4000);

            var acceptButton = driver.FindElement(By.XPath("//button[contains(text(),'Aceptar')]"));
            acceptButton.Click();
            Thread.Sleep(4000);

            // Validar éxito
            var successElement = wait.Until(drv => drv.FindElement(By.XPath("//img[@id='ImagenLogo']")));
            Assert.IsNotNull(successElement, "No se encontró el elemento de éxito después del login.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose(); // Libera memoria y recursos no administrados
        }
    }
}