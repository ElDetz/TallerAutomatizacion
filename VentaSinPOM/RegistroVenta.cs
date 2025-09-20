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
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        [Test]
        public void TestExampleSite()
        {
            // NAVEGAR EN URL
            _driver.Navigate().GoToUrl("https://taller2025-qa.sigesonline.com/");

            var usernameLocator = By.Id("Email");
            var passwordField = _driver.FindElement(By.XPath("//input[@id='Password']"));
            var loginButton = _driver.FindElement(By.XPath("//button[contains(text(),'Iniciar')]"));

            //INICIAR SESIÓN CON CREDENCIALES
            var usernameField = _wait.Until(ExpectedConditions.ElementIsVisible(usernameLocator));
            usernameField.SendKeys("admin@plazafer.com");
            passwordField.SendKeys("calidad");
            loginButton.Click();

            // BOTÓN ACEPTAR
            var acceptButton = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Aceptar')]")));
            acceptButton.Click();

            //1. SELECCIONAR EL MÓDULO VENTA
            var saleButton = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@class='menu-lista-cabecera']/span[text()='Venta']")));
            saleButton.Click();

            //2. SELECCIONAR EL SUB-MÓDULO NUEVA VENTA
            var newSaleButton = _driver.FindElement(By.XPath("//a[normalize-space()='Nueva Venta']"));
            newSaleButton.Click();
            Delay(15);

            //3. AGREGAR UN CONCEPTO "1010-3"
            try
            {
                var conceptSelection = By.XPath("/html/body/div[1]/div/section/div/div/div[1]/form/div[1]/div/div/registrador-detalles/div/div[1]/selector-concepto-comercial/ng-form/div/div[3]/div/div/span/span[1]/span");

                //var conceptSelection = By.XPath("//span[contains(@class,'select2-selection--single')]");
                _wait.Until(ExpectedConditions.ElementIsVisible(conceptSelection));
                IWebElement dropdown = _driver.FindElement(conceptSelection);
                dropdown.Click();
                var SelectODropdownOptions = By.CssSelector(".select2-results__options");
                _wait.Until(ExpectedConditions.ElementIsVisible(SelectODropdownOptions));
                IWebElement optionElement = _driver.FindElement(By.XPath($"//li[contains(text(), '{"1010-3"}')]"));
                optionElement.Click();
                Delay(2);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: No se encontró la opción '{"1010-3"}' en el menú desplegable. Detalle: {ex.Message}");
            }

            //4. INGRESAR LA CANTIDAD "2" 
            var conceptAmount = By.Id("cantidad-0");
            var element = _wait.Until(ExpectedConditions.ElementToBeClickable(conceptAmount));
            //element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys("2");

            //5. ACTIVAR IGV "SÍ"
            var igv = _driver.FindElement(By.Id("ventaigv0"));
            igv.Click();

            //6. INGRESAR CLIENTE "71310154"
            var client = By.Id("DocumentoIdentidad");
            var element2 = _driver.FindElement(client);
            element2.SendKeys(Keys.Control + "a");
            element2.SendKeys("71310154");
            element2.SendKeys(Keys.Enter);
            Delay(3);

            //7. SELECCIONAR TIPO DE COMPROBANTE "BOLETA"
            try
            {
                var voucher = By.XPath("/html/body/div[1]/div/section/div/div/div[1]/form/div[2]/facturacion-venta/form/div/div[2]/div/div[6]/selector-comprobante/div/ng-form/div[1]/div/span/span[1]/span");

                _wait.Until(ExpectedConditions.ElementIsVisible(voucher));
                IWebElement dropdown = _driver.FindElement(voucher);
                dropdown.Click();
                var SelectODropdownOptions = By.CssSelector(".select2-results__options");
                _wait.Until(ExpectedConditions.ElementIsVisible(SelectODropdownOptions));
                IWebElement optionElement = _driver.FindElement(By.XPath("//li[text()='BOLETA DE VENTA ELECTRONICA']"));
                optionElement.Click();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: No se encontró la opción '{"BOLETA"}' en el menú desplegable. Detalle: {ex.Message}");
            }
            //8. SELECCIONAR TIPO DE PAGO "CONTADO"
            Delay(3);
            var cashPayment = By.CssSelector("label[for='radio1']");
            _wait.Until(ExpectedConditions.ElementToBeClickable(cashPayment));
            _driver.FindElement(cashPayment).Click();

            //9. SELECCIONAR MEDIO DE PAGO "CONTADO"
            var debitCardButton = By.Id("labelMedioPago-0-18");
            _wait.Until(ExpectedConditions.ElementToBeClickable(debitCardButton));
            _driver.FindElement(debitCardButton).Click();

            //10. INGRESAR INFORMACIÓN DEL PAGO
            var information = _driver.FindElement(By.XPath("//div[@class='box box-primary box-solid']//textarea[@id='informacion']"));
            information.SendKeys("nro 5");

            //11. GUARDAR VENTA
            var saveSale = _driver.FindElement(By.XPath("//button[normalize-space()='GUARDAR VENTA']"));
            saveSale.Click();
            Delay(6);
        }

        //FUNCIONES AUXILIARES
        private By overlayLocator = By.XPath("//div[@class='block-ui-overlay']");
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
                    return !overlay.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}