
using ExampleSales.Pages;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using System;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace ExampleSales.StepDefinitions
{
    [Binding]
    public class AccesAppStepDefinitions
    {
        private IWebDriver driver;
        AccessPage accessPage;

        public AccesAppStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            accessPage = new AccessPage(driver);
        }

        [Given("el usuario ingresa al ambiente {string}")]
        public void GivenElUsuarioIngresaAlAmbiente(string _ambiente)
        {
            accessPage.OpenToApplication(_ambiente);
        }

        [Given("el usuario inicia sesión con usuario {string} y contraseña {string}")]
        public void GivenElUsuarioIniciaSesionConUsuarioYContrasena(string _user, string _password)
        {
            accessPage.LoginToApplication(_user, _password);
        }

        [Given("accede al módulo {string}")]
        public void GivenAccedeAlModulo(string _modulo)
        {
            accessPage.enterModulo(_modulo);
        }

        [Given("accede al submódulo {string}")]
        public void GivenAccedeAlSubmodulo(string _submodulo)
        {
            accessPage.enterSubModulo(_submodulo);
        }

    }
}
