using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExampleSales.Pages.Helpers
{
    public class Utilities
    {
        private IWebDriver driver;
        WebDriverWait wait;

        public Utilities(IWebDriver driver, int timeoutInSeconds = 50)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "El WebDriver no puede ser null.");
            }
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        private By overlayLocator = By.ClassName("block-ui-overlay"); // OVERLAY

        public void Delay(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public void WaitForOverlayToDisappear()
        {
            wait.Until(driver =>
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

        public string Formato(string accion, string input)
        {
            switch (accion.ToLower())
            {
                case "nombre":
                    // Si el string comienza con números seguidos de '|', lo eliminamos
                    return Regex.IsMatch(input, @"^\d+\|")
                        ? Regex.Replace(input, @"^\d+\|", "").Trim()
                        : input.Trim();

                case "numero":
                    // Expresión regular para capturar solo el número al inicio antes del "|"
                    Match match = Regex.Match(input, @"^(\d+)\|");

                    // Si hay coincidencia, devuelve el número; de lo contrario, devuelve "N/A"
                    return match.Success ? match.Groups[1].Value : "N/A";

                case "decimal":
                    if (decimal.TryParse(input, out decimal numero))
                    {
                        numero.ToString("0.00"); // Convierte a dos decimales
                    }
                    return input;
                default:
                    return "Acción inválida";
            }
        }

        public void InputTexto(By _path, string _field)
        {
            if (driver.FindElements(_path).Count == 0)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_path} no se encontró.");
            }

            wait.Until(ExpectedConditions.ElementIsVisible(_path)); // Espera hasta que el elemento sea visible
            driver.FindElement(_path).Clear();
            driver.FindElement(_path).SendKeys(_field);
        }

        public void SubmitTexto(By _path, string _field)
        {
            if (driver.FindElements(_path).Count == 0)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_path} no se encontró.");
            }

            wait.Until(ExpectedConditions.ElementIsVisible(_path)); // Espera hasta que el elemento sea visible
            driver.FindElement(_path).Clear();
            driver.FindElement(_path).SendKeys(_field);
            driver.FindElement(_path).SendKeys(Keys.Enter);
        }

        public void ClearAndInputText(By _field, string _value) // USADO PARA FECHAS
        {
            if (string.IsNullOrEmpty(_value)) // Verifica si el campo (_field) está vacío o nulo
            {
                return; // Si el campo está vacío, simplemente sale del método sin hacer nada
            }
            if (driver.FindElements(_field).Count == 0)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_field} no se encontró.");
            }
            wait.Until(ExpectedConditions.ElementIsVisible(_field));
            driver.FindElement(_field).SendKeys(Keys.Control + "a");
            driver.FindElement(_field).SendKeys(Keys.Delete);
            driver.FindElement(_field).Clear();
            driver.FindElement(_field).SendKeys(_value);
        }

        public void EstablecerFechaAngular(By locator, string fecha)
        {
            IWebElement input = driver.FindElement(locator);

            string script = @"
                var input = arguments[0];
                var valor = arguments[1];
                input.value = valor;
                input.dispatchEvent(new Event('input', { bubbles: true }));
                input.dispatchEvent(new Event('blur'));
            ";

            ((IJavaScriptExecutor)driver).ExecuteScript(script, input, fecha);
        }

        public void VisibilidadElement(By _path)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(_path));
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_path} no fue visible dentro del tiempo esperado.");
            }
        }

        public void ElementExists(By _button)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementExists(_button)); // Espera hasta que el elemento exista en el DOM
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_button} no se encontró en el DOM dentro del tiempo esperado.");
            }
        }

        public void ClickButton(By locator)
        {
            var elements = driver.FindElements(locator);
            if (elements.Count == 0)
                throw new InvalidOperationException($"El elemento con el localizador '{locator}' no se encontró.");

            Delay(2);
            WaitForOverlayToDisappear();

            var element = elements.First();
            if (!element.Displayed || !element.Enabled)
                throw new InvalidOperationException($"El botón con el localizador '{locator}' está deshabilitado o no visible.");

            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }

        // MODALES
        public void ClickButtonInModal(IWebElement _element, By buttonLocator)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(buttonLocator));
            wait.Until(ExpectedConditions.ElementToBeClickable(buttonLocator)); // Espera hasta que el elemento sea clickeable
            _element.FindElement(buttonLocator).Click();
            Delay(2);
        }

        public void InputTextoModal(IWebElement _element, By _path, string _field)
        {
            if (_element.FindElements(_path).Count == 0)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_path} no se encontró.");
            }

            // wait.Until(ExpectedConditions.ElementIsVisible(_path)); // Espera hasta que el elemento sea visible
            _element.FindElement(_path).Clear();
            _element.FindElement(_path).SendKeys(_field);
            _element.FindElement(_path).SendKeys(Keys.Enter);
            Delay(2);
        }

        public void addFieldModal(IWebElement _element, By _path, string _field)
        {
            if (string.IsNullOrEmpty(_field)) // Verifica si el campo (_field) está vacío o nulo
            {
                return; // Si el campo está vacío, simplemente sale del método sin hacer nada
            }

            if (_element.FindElements(_path).Count == 0)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_path} no se encontró.");
            }

            // wait.Until(ExpectedConditions.ElementIsVisible(_path)); // Espera hasta que el elemento sea visible
            _element.FindElement(_path).Clear();
            Delay(2);
            _element.FindElement(_path).SendKeys(_field);
            Delay(2);
        }

        // SCROLL
        public void ScrollViewElement(IWebElement _path)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", _path);
            Delay(2);
        }

        public void ScrollViewElementCentered(IWebElement _path)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("var element = arguments[0]; var rect = element.getBoundingClientRect(); window.scrollBy({top: rect.top + window.scrollY - (window.innerHeight / 2), behavior: 'smooth'});", _path);
            Delay(2);
        }

        // CENTRAR ELEMENTO - USADO PARA LAS CARTILLAS
        public void ScrollElement(IWebElement _path)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'nearest', inline: 'center'});", _path);
            Delay(4);
        }

        // SCROLL HACIA ARRIBA
        public void ScrollViewTop()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Delay(2);
        }

        // SCROLL HACIA ABAJO 
        public void ScrollViewBottom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            Delay(2);
        }

        public void SelecOption(IWebElement _element, By _path, string option)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(_path));
                wait.Until(ExpectedConditions.ElementToBeClickable(_path));

                // Espera explícita para que las opciones sean visibles
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".select2-results__options")));

                _element.FindElement(_path).Click();
                // Selecciona la opción deseada
                IWebElement optionElement = _element.FindElement(By.XPath($"//li[contains(text(), '{option}')]"));
                optionElement.Click();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: No se encontró la opción '{option}' en el menú desplegable. Detalle: {ex.Message}");
            }
        }

        public void SelecOptionInModal(IWebElement _element, By _path, string _option) //Seleccion de una opcion en Modal
        {
            if (string.IsNullOrEmpty(_option)) // Verifica si el campo (_field) está vacío o nulo
            {
                return; // Si el campo está vacío, simplemente sale del método sin hacer nada
            }
            var dropdown = new SelectElement(_element.FindElement(_path));
            dropdown.SelectByText(_option);
            Assert.That(dropdown.SelectedOption.Text, Is.EqualTo(_option));
            Delay(2);
        }

        //Helpers
        public class Path()
        {
            public static readonly By SelectODropdownOptions = By.CssSelector(".select2-results__options");
            public static readonly By OverlayElement = By.ClassName("block-ui-overlay");

        }
        public void Overlay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.block-ui-overlay")));
        }

        public void EnterField(By _path, string _field)
        {
            var element = driver.FindElement(_path);
            element.Click();  // Asegura que el campo esté activo

            // Borra el campo completamente con Ctrl + A y Suprimir
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);

            // Espera un momento para asegurarse de que el campo esté vacío
            Thread.Sleep(100);

            // Verifica si el campo quedó vacío antes de ingresar el nuevo texto
            if (!string.IsNullOrEmpty(element.GetAttribute("value")))
            {
                element.Clear();
            }

            // Ingresa el nuevo valor
            element.SendKeys(_field);
            element.SendKeys(Keys.Enter);
            Thread.Sleep(4000);
        }

        public void SelectComboBox(By _path, string data)
        {
            // Ubicar el elemento <select>
            IWebElement selectElement = driver.FindElement(_path);

            // Crear el objeto SelectElement
            SelectElement dropdown = new SelectElement(selectElement);

            // Seleccionar el ROL pasado como parámetro
            dropdown.SelectByText(data);

            // Validar que la opción seleccionada es la esperada
            Assert.That(dropdown.SelectedOption.Text, Is.EqualTo(data),
                $"La opción seleccionada '{dropdown.SelectedOption.Text}' no coincide con '{data}'");

            // Pequeña pausa para visualización (opcional)
            Thread.Sleep(1000);
        }
        public void WaitForElementVisible(By locator)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException($"El elemento con el localizador {locator} no se hizo visible dentro del tiempo de espera.");
            }
        }

        public void WaitExistsVisible(By pathComponent, By pathOverlay)
        {
            ElementExists(pathComponent);
            WaitForElementVisible(pathOverlay);
        }

        public void SelectOption(By pathComponent, string option)
        {

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(pathComponent));

                IWebElement dropdown = driver.FindElement(pathComponent);
                dropdown.Click();

                wait.Until(ExpectedConditions.ElementIsVisible(Path.SelectODropdownOptions));

                IWebElement optionElement = driver.FindElement(By.XPath($"//li[contains(text(), '{option}')]"));
                optionElement.Click();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: No se encontró la opción '{option}' en el menú desplegable. Detalle: {ex.Message}");
            }
        }
    }
}
