using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaPOM.Pages.Helpers;

namespace VentaPOM.Pages
{
    public class AccesoPage
    {
        private IWebDriver driver;
        Utilities utilities;

        public AccesoPage(IWebDriver driver)
        {
            this.driver = driver;
            utilities = new Utilities(driver);
        }

        // LOCALIZADORES LOGIN
        private By usernameField = By.XPath("//input[@id='Email']"); 
        private By passwordField = By.XPath("//input[@id='Password']"); 
        private By loginButton = By.XPath("//button[contains(text(),'Iniciar')]");
        private By acceptButton = By.XPath("//button[contains(text(),'Aceptar')]");

        // LOCALIZADORES VENTA
        private By VentaField = By.XPath("//a[@class='menu-lista-cabecera']/span[text()='Venta']");
        private By NuevaVentaField = By.XPath("//a[normalize-space()='Nueva Venta']");
        public void OpenToApplication(string _ambiente)
        {
            driver.Url = _ambiente;
        }

        public void LoginToApplication(string _user, string _password)
        {
            utilities.EnterText(usernameField, _user);
            utilities.EnterText(passwordField, _password);
            utilities.Delay(2);
            utilities.ClickButton(loginButton);
            utilities.Delay(2);
            utilities.VisibilidadElement(acceptButton);
            utilities.ClickButton(acceptButton);
        }

        public void enterModulo(string _modulo)
        {
            utilities.Delay(2);

            switch (_modulo)
            {
                case "Venta":
                    utilities.VisibilidadElement(VentaField);
                    utilities.ClickButton(VentaField);

                    break;

                default:
                    throw new ArgumentException($"El {_modulo} no es válido.");
            }
        }

        public void enterSubModulo(string _subModulo)
        {
            utilities.Delay(2);

            switch (_subModulo)
            {
                case "Nueva Venta":
                    utilities.VisibilidadElement(NuevaVentaField);
                    utilities.ClickButton(NuevaVentaField);

                    break;

                default:
                    throw new ArgumentException($"El {_subModulo} no es válido.");
            }
        }
    }
}
