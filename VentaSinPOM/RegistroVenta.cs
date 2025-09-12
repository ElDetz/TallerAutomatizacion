using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;
namespace VentaSinPOM
{
    [TestFixture]
    public class NewSale
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestExampleSite()
        {
            // NAVEGAR EN URL
            _driver.Navigate().GoToUrl("https://taller2025-qa.sigesonline.com/");

            Thread.Sleep(4000);

            // LOCALIZADORES DE ELEMENTOS 
            var usernameField = _driver.FindElement(By.XPath("//input[@id='Email']"));
            var passwordField = _driver.FindElement(By.XPath("//input[@id='Password']"));
            var loginButton = _driver.FindElement(By.XPath("//button[contains(text(),'Iniciar')]"));

            //INICIAR SESIÓN CON CREDENCIALES
            usernameField.SendKeys("admin@plazafer.com");
            Thread.Sleep(2000);
            passwordField.SendKeys("calidad");
            Thread.Sleep(2000);
            loginButton.Click(); // Ingresar
            Thread.Sleep(4000);

            // BOTÓN ACEPTAR
            var acceptButton = _driver.FindElement(By.XPath("//button[contains(text(),'Aceptar')]"));
            acceptButton.Click();
            Thread.Sleep(4000);

            //INGRESAR AL MÓDULO VENTA
            var saleButton = _driver.FindElement(By.XPath("//a[@class='menu-lista-cabecera']/span[text()='Venta']"));
            saleButton.Click();

            //INGRESAR AL SUBMÓDULO NUEVA VENTA
            var newSaleButton = _driver.FindElement(By.XPath("//a[normalize-space()='Nueva Venta']"));
            newSaleButton.Click();
            Thread.Sleep(9000);

            //SELECCIONAR UN PRODUCTO
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var conceptSelection = By.XPath("//body/div[@id='wrapper']/div[1]/section[1]/div[1]/div[1]/div[1]/form[1]/div[1]/div[1]/div[1]/registrador-detalles[1]/div[1]/div[1]/selector-concepto-comercial[1]/ng-form[1]/div[1]/div[3]/div[1]/div[1]/span[1]/span[1]/span[1]");
                wait.Until(ExpectedConditions.ElementIsVisible(conceptSelection));
                IWebElement dropdown = _driver.FindElement(conceptSelection);
                dropdown.Click();
                var SelectODropdownOptions = By.CssSelector(".select2-results__options");
                wait.Until(ExpectedConditions.ElementIsVisible(SelectODropdownOptions));
                IWebElement optionElement = _driver.FindElement(By.XPath($"//li[contains(text(), '{"1010-3"}')]"));
                optionElement.Click();
                Thread.Sleep(2000);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: No se encontró la opción '{"1010-3"}' en el menú desplegable. Detalle: {ex.Message}");
            }

            //INGRESAR LA CANTIDAD DEL PRODUCTO
            var conceptAmount = By.Id("cantidad-0");
            var element = _driver.FindElement(conceptAmount);
            element.Click();
            element.SendKeys(Keys.Control + "a");
            Thread.Sleep(100);
            element.SendKeys("2");

            //ACTIVAR IGV
            var igv = _driver.FindElement(By.Id("ventaigv0"));
            igv.Click();
            Thread.Sleep(1000);

            //SELECCIONAR EL DOC. DEL CLIENTE
            var client = By.Id("DocumentoIdentidad");
            var element2 = _driver.FindElement(client);
            element2.Click();
            element2.SendKeys(Keys.Control + "a");
            Thread.Sleep(100);
            element2.SendKeys("72380461");
            element2.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            //SELECCIONAR COMPROBANTE
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var voucher = By.XPath("//body/div[@id='wrapper']/div[1]/section[1]/div[1]/div[1]/div[1]/form[1]/div[2]/facturacion-venta[1]/form[1]/div[1]/div[2]/div[1]/div[6]/selector-comprobante[1]/div[1]/ng-form[1]/div[1]/div[1]/span[1]/span[1]/span[1]");
                wait.Until(ExpectedConditions.ElementIsVisible(voucher));

                IWebElement dropdown = _driver.FindElement(voucher);
                dropdown.Click();
                var SelectODropdownOptions = By.CssSelector(".select2-results__options");
                wait.Until(ExpectedConditions.ElementIsVisible(SelectODropdownOptions));

                IWebElement optionElement = _driver.FindElement(By.XPath($"//li[contains(text(), '{"NOTA"}')]"));
                optionElement.Click();
                Thread.Sleep(2000);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: No se encontró la opción '{"NOTA"}' en el menú desplegable. Detalle: {ex.Message}");
            }

            //SELECCIONAR MEDIO DE PAGO
            var DebitCardButton = By.Id("labelMedioPago-0-18");
            ClickButton(DebitCardButton);
            Thread.Sleep(3000);

            //INGRESAR INFORMACIÓN
            var information = _driver.FindElement(By.XPath("//div[@class='box box-primary box-solid']//textarea[@id='informacion']"));
            information.SendKeys("nro 5");

            //GUARDAR VENTA
            var saveSale = _driver.FindElement(By.XPath("//button[normalize-space()='GUARDAR VENTA']"));
            saveSale.Click();
            Thread.Sleep(6000);
        }

        //FUNCIONES REUTILIZABLES
        private By overlayLocator = By.ClassName("block-ui-overlay");
        public void Delay(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
        public void WaitForOverlayToDisappear()
        {
            _wait.Until(driver =>
            {
                try
                {
                    IWebElement overlay = driver.FindElement(overlayLocator);
                    return !overlay.Displayed; // Espera hasta que el overlay no esté visible
                }
                catch (NoSuchElementException)
                {
                    return true; // Si no se encuentra, el overlay ya desapareció
                }
            });
        }

        public bool ClickButton(By _button)
        {
            try
            {
                if (_driver.FindElements(_button).Count == 0)
                {
                    Console.WriteLine($"[INFO] El elemento con el localizador {_button} no se encontró.");
                    return false;
                }

                Delay(2);
                WaitForOverlayToDisappear();

                // Verifica si el botón está visible y habilitado antes de intentar hacer clic
                var element = _driver.FindElement(_button);
                if (!element.Displayed || !element.Enabled)
                {
                    Console.WriteLine($"[INFO] El botón con el localizador {_button} está deshabilitado o no visible. No se puede hacer clic.");
                    return false;
                }

                _wait.Until(ExpectedConditions.ElementToBeClickable(_button));
                element.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] No se pudo hacer clic en el botón {_button}: {ex.Message}");
                return false;
            }
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