using ExampleSales.Pages.Ventas;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace ExampleSales.StepDefinitions
{
    [Binding]
    public class RegistrarUnaVentaStepDefinitions
    {
        private IWebDriver driver;
        VentasPage newSale;

        public RegistrarUnaVentaStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            this.newSale = new VentasPage(driver);
        }
        [When("el usuario agrega el concepto {string}")]
        public void WhenElUsuarioAgregaElConcepto(string concept)
        {
            newSale.SelectConcept(concept);
        }

        [When("ingresa la cantidad {string}")]
        public void WhenIngresaLaCantidad(string amount)
        {
            newSale.InputAmount(amount);
        }

        [When("selecciona al cliente con documento {string}")]
        public void WhenSeleccionaAlClienteConDocumento(string dni)
        {
            newSale.EnterCustomer(dni);
        }

        [When("selecciona el tipo de comprobante {string}")]
        public void WhenSeleccionaElTipoDeComprobante(string option)
        {
            newSale.SelectTypeDocument(option);
        }

        [When("selecciona el tipo de pago {string}")]
        public void WhenSeleccionaElTipoDePago(string option)
        {
            newSale.SelectPaymentType(option);
        }

        [When("selecciona el medio de pago {string}")]
        public void WhenSeleccionaElMedioDePago(string option)
        {
            newSale.PaymentMethod(option);
        }

        [When("registra los datos del pago con número {string}")]
        public void WhenRegistraLosDatosDelPagoConNumero(string option)
        {
            newSale.PaymentMethod(option);
        }

        [Then("la venta se guarda correctamente")]
        public void ThenLaVentaSeGuardaCorrectamente()
        {
            newSale.SaveSale();
        }

    }
}
