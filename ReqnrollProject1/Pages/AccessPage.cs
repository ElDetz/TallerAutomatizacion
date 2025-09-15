using ExampleSales.Pages.Helpers;
using OpenQA.Selenium;

namespace ExampleSales.Pages
{
    public class AccessPage
    {
        private IWebDriver driver;
        Utilities utilities;

        public AccessPage(IWebDriver driver)
        {
            this.driver = driver;
            utilities = new Utilities(driver);
        }

        // LOGIN
        private By usernameField = By.XPath("//input[@id='Email']"); // campo de usuario
        private By passwordField = By.XPath("//input[@id='Password']"); // campo de contraseña
        private By loginButton = By.XPath("//button[contains(text(),'Iniciar')]"); // botón de inicio de sesión 
        private By acceptButton = By.XPath("//button[contains(text(),'Aceptar')]"); // botón de aceptar

        // INGRESO MODULO RESTAURANTE
        private By VentaField = By.XPath("//a[@class='menu-lista-cabecera']/span[text()='Venta']");
        private By NuevaVentaField = By.XPath("//a[normalize-space()='Nueva Venta']");


        // INICIO DE SESION
        public void LoginToApplication(string _user, string _password)
        {
            driver.Url = "https://taller2025-qa.sigesonline.com/";
            utilities.InputTexto(usernameField, _user); // campo user
            utilities.InputTexto(passwordField, _password); // campo contraseña
            utilities.ClickButton(loginButton); // boton login
            utilities.VisibilidadElement(acceptButton);
            utilities.ClickButton(acceptButton); // boton aceptar
        }

        public void enterModulo(string _modulo)
        {
            utilities.Delay(2);
            utilities.ClickButton(VentaField);
            utilities.WaitForOverlayToDisappear();
            utilities.Delay(2);

            switch (_modulo)
            {
                case "Nueva Venta":
                    utilities.VisibilidadElement(NuevaVentaField);
                    utilities.ClickButton(NuevaVentaField);

                    break;

                default:
                    throw new ArgumentException($"El {_modulo} no es válido.");
            }

            utilities.Delay(4);
        }
    }
}
