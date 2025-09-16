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
        [Given("Agregar concepto: {string}")]
        public void GivenAgregarConcepto(string concept)
        {
            newSale.SelectConcept(concept);
        }

        [Given("Ingreso la cantidad {string}")]
        public void GivenIngresoLaCantidad(string amount)
        {
            newSale.InputAmount(amount);
        }

        [Given("Activar IGV {string}")]
        public void GivenActivarIGV(string option)
        {
            newSale.SelectIGV(option);
        }

        [Given("Ingresar Cliente {string}")]
        public void GivenIngresarCliente(string dni)
        {
            newSale.EnterCustomer(dni);
        }

        [Given("Seleccionar Tipo de Comprobante {string}")]
        public void GivenSeleccionarTipoDeComprobante(string option)
        {
            newSale.SelectTypeDocument(option);
        }

        [Given("Seleccionar Tipo de pago {string}")]
        public void GivenSeleccionarTipoDePago(string option)
        {
            newSale.SelectPaymentType(option);
        }

        [Given("Seleccionar Medio de Pago {string}")]
        public void GivenSeleccionarMedioDePago(string option)
        {
            newSale.PaymentMethod(option);
        }

        [Given("Ingresar Datos del Pago: {string}")]
        public void GivenIngresarDatosDelPago(string value)
        {
            newSale.InformationPayment(value);
        }

        [Then("Guardar venta")]
        public void ThenGuardarVenta()
        {
            newSale.SaveSale();
        }

    }
}
