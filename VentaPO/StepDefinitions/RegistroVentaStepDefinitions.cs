using Microsoft.VisualBasic.FileIO;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VentaPOM.Pages;

namespace VentaPOM.StepDefinitions
{
    [Binding]
    public class RegistroVentaStepDefinitions
    {
        private IWebDriver driver;
        RegistroVentaPage newSale;

        public RegistroVentaStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            this.newSale = new RegistroVentaPage(driver);
        }

        [When("el usuario agrega el concepto {string}")]
        public void WhenElUsuarioAgregaElConcepto(string value)
        {
            newSale.SelectConcept(value);
        }

        [When("ingresa la cantidad {string}")]
        public void WhenIngresaLaCantidad(string value)
        {
            newSale.InputAmount(value);
        }
        [When("selecciona igv")]
        public void WhenSeleccionaIgv()
        {
            newSale.SelectIGV();
        }

        [When("selecciona al cliente con documento {string}")]
        public void WhenSeleccionaAlClienteConDocumento(string value)
        {
            newSale.EnterCustomer(value);
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

        [When("ingrese la informacion del pago {string}")]
        public void WhenIngreseLaInformacionDelPago(string value)
        {
            newSale.InformationPayment(value);
        }

        [Then("la venta se guarda correctamente")]
        public void ThenLaVentaSeGuardaCorrectamente()
        {
            newSale.SaveSale();
        }
    }
}
