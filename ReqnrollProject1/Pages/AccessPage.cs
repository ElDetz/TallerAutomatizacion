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

        private By RestauranteField = By.XPath("//a[@class='menu-lista-cabecera']/span[text()='Restaurante']");
        private By AtencionField = By.XPath("//a[normalize-space()='Atención']");

        public void OpenToApplication(string _ambiente)
        {
            driver.Url = _ambiente;
        }

        // INICIO DE SESION
        public void LoginToApplication(string _user, string _password)
        {
            utilities.EnterText(usernameField, _user); // campo user
            utilities.EnterText(passwordField, _password); // campo contraseña
            utilities.ClickB(loginButton); // boton login
            utilities.VisibilidadElement(acceptButton);
            utilities.ClickB(acceptButton); // boton aceptar
        }

        public void enterModulo(string _modulo)
        {
            utilities.Delay(2);

            switch (_modulo)
            {
                case "Venta":
                    utilities.VisibilidadElement(VentaField);
                    utilities.ClickB(VentaField);

                    break;

                case "Restaurante":
                    utilities.VisibilidadElement(RestauranteField);
                    utilities.ClickB(RestauranteField);

                    break;

                default:
                    throw new ArgumentException($"El {_modulo} no es válido.");
            }

            utilities.Delay(4);
        }

        public void enterSubModulo(string _subModulo)
        {
            utilities.Delay(2);

            switch (_subModulo)
            {
                case "Nueva Venta":
                    utilities.VisibilidadElement(NuevaVentaField);
                    utilities.ClickB(NuevaVentaField);

                    break;

                case "Atención":
                    utilities.VisibilidadElement(AtencionField);
                    utilities.ClickB(AtencionField);

                    break;

                default:
                    throw new ArgumentException($"El {_subModulo} no es válido.");
            }

            utilities.Delay(2);
        }
    }
}
