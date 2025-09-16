using ExampleSales.Pages.Ventas;
using OpenQA.Selenium;
using Reqnroll;
using System;
using VentaConPOM.Pages.Restaurante;

namespace VentaConPOM.StepDefinitions
{
    [Binding]
    public class TomarAtencionStepDefinitions
    {
        private IWebDriver driver;
        TomarAtencionPage tomarAtencionPage;

        public TomarAtencionStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            tomarAtencionPage = new TomarAtencionPage(driver);
        }

        [Given("Se seleciona el tipo de atencion {string}")]
        public void GivenSeSelecionaElTipoDeAtencion(string _tipoAtencion)
        {
            tomarAtencionPage.TipoAtencion(_tipoAtencion);
        }

        [Given("Se selecciona el ambiente {string}")]
        public void GivenSeSeleccionaElAmbiente(string _ambiente)
        {
            tomarAtencionPage.SeleccionAmbiente(_ambiente);
        }

        [Given("Seleccion de la mesa {string} en estado {string}")]
        public void GivenSeleccionDeLaMesaEnEstado(string _nMesa, string _estado)
        {
            tomarAtencionPage.SeleccionMesa(_nMesa, _estado);
        }

        [Given("Se selecciona el mozo {string}")]
        public void GivenSeSeleccionaElMozo(string _mozo)
        {
            tomarAtencionPage.SeleccionMozo(_mozo);
        }

        [When("Se ingresa las siguientes ordenes:")]
        public void WhenSeIngresaLasSiguientesOrdenes(DataTable dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                string _orden = row["Orden"];
                string _concepto = row["Concepto"];
                string _cantidad = row["Cantidad"];
                string _anotacion = row["Anotacion"];

                switch (_orden)
                {
                    case "CODIGO":
                        tomarAtencionPage.IngresarCodigoItem(tomarAtencionPage.Formato("numero", _concepto));
                        break;

                    case "ITEM":
                        tomarAtencionPage.SeleccionItem(_concepto, _cantidad);
                        break;

                    default:
                        throw new ArgumentException($"ORDEN NO INGRESADA: {_orden}");
                }

                if (!string.IsNullOrWhiteSpace(_anotacion))
                {
                    tomarAtencionPage.DetalleItem("Agregar anotacion", tomarAtencionPage.Formato("nombre", _concepto), _cantidad, _anotacion);
                }
            }
        }

        [Then("Se procede a {string} la orden {string}")]
        public void ThenSeProcedeALaOrden(string _opcionOrden, string _nOrden)
        {
            tomarAtencionPage.OpcionOrden(_opcionOrden, _nOrden);
        }
    }
}
