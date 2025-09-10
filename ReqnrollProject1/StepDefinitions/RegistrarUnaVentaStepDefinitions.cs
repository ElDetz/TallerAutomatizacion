using ExampleSales.Pages.Ventas;
using OpenQA.Selenium;
using Reqnroll;
using System;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

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

        /*[Given("Agregar Concepto {string}")]
        public void GivenAgregarConcepto(string concept)
        {
            newSale.SelectConcept(concept);
        }*/

        [Given("Agregar concepto:")]
        public void WhenAgregarConcepto(DataTable table)
        {
            foreach (var row in table.Rows)
            {
                string option = row["Opción"];
                string value = row["Valor"];

                newSale.TypeSelectConcept(option, value);
            }
        }



        [Given("Ingresar Cantidad {string} y Precio Unitario {string}")]
        public void GivenIngresarCantidadYPrecioUnitario(string amount, string price)
        {
            newSale.InputAmountAndPrice(amount, price);
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
        public void GivenIngresarDatosDelPago(string option)
        {
            newSale.InformationPayment(option);
        }

        [Then("Guardar venta")]
        public void ThenGuardarVenta()
        {
            newSale.SaveSale();
        }

    }
}
