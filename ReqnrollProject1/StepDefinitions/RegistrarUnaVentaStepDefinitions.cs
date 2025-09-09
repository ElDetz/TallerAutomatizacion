using System;
using Reqnroll;

namespace ExampleSales.StepDefinitions
{
    [Binding]
    public class RegistrarUnaVentaStepDefinitions
    {
        [Given("Agregar concepto:")]
        public void GivenAgregarConcepto(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("Ingresa los siguientes datos del producto:")]
        public void GivenIngresaLosSiguientesDatosDelProducto(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("Activar IGV {string}")]
        public void GivenActivarIGV(string sI)
        {
            throw new PendingStepException();
        }

        [Given("Seleccionar tipo de cliente:")]
        public void GivenSeleccionarTipoDeCliente(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("Seleccionar tipo de comprobante")]
        public void GivenSeleccionarTipoDeComprobante(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("Seleccionar tipo de pago {string}")]
        public void GivenSeleccionarTipoDePago(string contado)
        {
            throw new PendingStepException();
        }

        [Given("Seleccionar el medio de pago {string}")]
        public void GivenSeleccionarElMedioDePago(string tDEB)
        {
            throw new PendingStepException();
        }

        [Given("Rellene datos de la tarjeta:")]
        public void GivenRelleneDatosDeLaTarjeta(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Then("Guardar venta")]
        public void ThenGuardarVenta()
        {
            throw new PendingStepException();
        }
    }
}
