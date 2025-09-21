using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaPOM.Pages.Helpers
{
    public class Utilities
    {
        private IWebDriver driver;
        WebDriverWait wait;

        public Utilities(IWebDriver driver, int timeoutInSeconds = 50)
        {
           /* if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "El WebDriver no puede ser null.");
            }*/
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }
        private By overlayLocator = By.ClassName("block-ui-overlay"); // OVERLAY

        public class Path()
        {
            public static readonly By SelectODropdownOptions = By.CssSelector(".select2-results__options");
            public static readonly By OverlayElement = By.ClassName("block-ui-overlay");

        }
        public void Delay(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
        public void EnterText(By _path, string _field)
        {
            var element = driver.FindElement(_path);
            element.SendKeys(_field);
        }

        public void ClearAndEnterText(By _path, string _field)
        {
            var element = driver.FindElement(_path);
            element.SendKeys(Keys.Control + "a");
            EnterText(_path, _field);
        }

        public void ClearEnterTextAndSubmit(By _path, string _field)
        {
            var element = driver.FindElement(_path);
            ClearAndEnterText(_path, _field);
            element.SendKeys(Keys.Enter);
            Delay(3);
        }

        public void ClickButton(By _path)
        {
            var element = driver.FindElement(_path);
            element.Click();
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
    }
}
