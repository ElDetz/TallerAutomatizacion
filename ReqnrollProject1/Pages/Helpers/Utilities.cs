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

        /*
        public void ClickButton(By _button)
        {
            if (driver.FindElements(_button).Count == 0)
            {
                throw new NoSuchElementException($"El elemento con el localizador {_button} no se encontró.");
            }
            Delay(2);
            WaitForOverlayToDisappear(); // OVERLAY
            wait.Until(ExpectedConditions.ElementToBeClickable(_button)); // Espera hasta que el elemento sea clickeable
            driver.FindElement(_button).Click();
        }
        */
        public bool ClickButton(By _button)
        {
            try
            {

                if (driver.FindElements(_button).Count == 0)
                {
                    Console.WriteLine($"[INFO] El elemento con el localizador {_button} no se encontró.");
                    return false;
                }

                Delay(2);
                WaitForOverlayToDisappear();

                // Verifica si el botón está visible y habilitado antes de intentar hacer clic
                var element = driver.FindElement(_button);
                if (!element.Displayed || !element.Enabled)
                {
                    Console.WriteLine($"[INFO] El botón con el localizador {_button} está deshabilitado o no visible. No se puede hacer clic.");
                    return false;
                }

                wait.Until(ExpectedConditions.ElementToBeClickable(_button));
                element.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] No se pudo hacer clic en el botón {_button}: {ex.Message}");
                return false;
            }
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


                /*
                // Abre el menú desplegable
                IWebElement dropdown = _element.FindElement(_path);
                //dropdown.Click();
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", dropdown);
                */

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
    }
}
