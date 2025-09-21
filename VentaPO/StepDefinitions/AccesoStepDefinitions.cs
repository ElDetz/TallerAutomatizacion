
using OpenQA.Selenium;
using VentaPOM.Pages;
namespace VentaPOM.StepDefinitions
{
    [Binding]
    public class AccesoStepDefinitions
    {
        private IWebDriver driver;
        AccesoPage accessPage;

        public AccesoStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            accessPage = new AccesoPage(driver);
        }

        [Given("el usuario ingresa al ambiente {string}")]
        public void GivenElUsuarioIngresaAlAmbiente(string _ambiente)
        {
            accessPage.OpenToApplication(_ambiente);
        }


        [Given("el usuario inicia sesion con usuario {string} y contrasena {string}")]
        public void GivenElUsuarioIniciaSesionConUsuarioYContrasena(string _user, string _password)
        {
            accessPage.LoginToApplication(_user, _password);
        }

        [Given("accede al modulo {string}")]
        public void GivenAccedeAlModulo(string _modulo)
        {
            accessPage.enterModulo(_modulo);
        }

        [Given("accede al submodulo {string}")]
        public void GivenAccedeAlSubmodulo(string _submodulo)
        {
            accessPage.enterSubModulo(_submodulo);
        }

    }
}
