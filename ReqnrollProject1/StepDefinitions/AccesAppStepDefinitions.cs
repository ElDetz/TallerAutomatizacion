
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using ExampleSales.Pages;
using System;

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

        [Given(@"Inicio de sesión con usuario '([^']*)' y contraseña '([^']*)'")]
        public void Login(string _user, string _password)
        {
            accessPage.LoginToApplication(_user, _password);
        }

        [Given("Se ingresa al módulo {string}")]
        public void GivenSeIngresaAlModulo(string _modulo)
        {
            accessPage.enterModulo(_modulo);
        }


        [When("Se ingresa al módulo {string}")]
        public void WhenSeIngresaAlModulo(string _modulo)
        {
            accessPage.enterModulo(_modulo);
        }
    }
}
