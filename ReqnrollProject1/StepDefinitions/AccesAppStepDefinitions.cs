
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

        [Given("Ingreso al ambiente {string}")]
        public void GivenIngresoAlAmbiente(string _ambiente)
        {
            accessPage.OpenToApplication(_ambiente);
        }

        [Given(@"Inicio de sesi�n con usuario '([^']*)' y contrase�a '([^']*)'")]
        public void Login(string _user, string _password)
        {
            accessPage.LoginToApplication(_user, _password);
        }

        [Given("Se ingresa al m�dulo {string}")]
        public void GivenSeIngresaAlModulo(string _modulo)
        {
            accessPage.enterModulo(_modulo);
        }

        [Given("Se ingresa al subm�dulo {string}")]
        public void GivenSeIngresaAlSubmodulo(string _submodulo)
        {
            accessPage.enterSubModulo(_submodulo);
        }

    }
}
