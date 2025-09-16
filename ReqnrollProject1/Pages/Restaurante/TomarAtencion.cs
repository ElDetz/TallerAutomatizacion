using ExampleSales.Pages.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VentaConPOM.Pages.Restaurante
{
    public class TomarAtencionPage
    {
        private IWebDriver driver;
        Utilities utilities;

        public TomarAtencionPage(IWebDriver driver)
        {
            this.driver = driver;
            this.utilities = new Utilities(driver);
        }

        // TIPO DE ATENCION
        private By _atencionConMesa = By.XPath("//label[@id='labelatencionconmesa']");
        private By _atencionSinMesa = By.XPath("//label[@for='atencionsinmesa']");

        // AMBIENTE
        private By _ambientePrincipal = By.Id("ambienteconmesa-0");
        private By _ambienteReservaciones = By.Id("ambienteconmesa-1");

        // DETALLES PEDIDO
        private By _selectMozo = By.Id("mozo");
        private By _codeBarraItem = By.XPath("//input[@id='codigoBarraItem']");
        private By _selectItem = By.Id("itemSeleccionado");
        private By _itemCantidad = By.Id("cantidadItem");

        // ITEM
        private By _agregarItem = By.XPath("//button[@title='Agregar Item']");

        // ORDEN
        private By _guardarOrden = By.XPath("//p[@title='Guardar Orden']");
        private By _atenderTodo = By.XPath(".//button[@title='Atender todo']");
        private By _anularTodo = By.XPath(".//button[@title='Anular todo']");
        private By _reanudarOrden = By.XPath(".//button[@title='Reanudar todo']");
        private By _cerrarOrden = By.XPath(".//button[@title='Cerrar Orden']");

        public void TipoAtencion(string _atencion)
        {
            utilities.Delay(2);
            By fieldLocator;
            switch (_atencion)
            {
                case "Con mesa":
                    fieldLocator = _atencionConMesa;
                    break;

                case "Sin mesa":
                    fieldLocator = _atencionSinMesa;
                    break;

                default:
                    throw new ArgumentException($"El {_atencion} no es válido");
            }

            utilities.ClickButton(fieldLocator);
            utilities.Delay(2);
        }

        public void SeleccionAmbiente(string _ambiente)
        {
            By fieldLocator;
            switch (_ambiente)
            {
                case "PRINCIPAL":
                    fieldLocator = _ambientePrincipal;
                    return;

                case "RESERVACIONES":

                    fieldLocator = _ambienteReservaciones;
                    break;

                default:
                    throw new ArgumentException($"El {_ambiente} no es válido");
            }

            utilities.ElementExists(fieldLocator);
            utilities.ClickButton(fieldLocator);
            utilities.Delay(6);
        }

        public void SeleccionMesa(string _nMesa, string _estado)
        {
            var mesas = driver.FindElements(By.XPath($"//button[contains(@class, 'btn-mesa-{_estado}') and span]"));

            if (mesas.Count > 0)
            {
                bool mesaEncontrada = false;
                foreach (var mesa in mesas)
                {
                    try
                    {
                        var span = mesa.FindElement(By.TagName("span"));
                        var textoMesa = span.Text.Trim();
                        if (textoMesa == _nMesa) // Verificar si el texto coincide
                        {
                            mesa.Click();
                            mesaEncontrada = true;
                            break;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("NO SE ENCONTRO EL ELEMENTO");
                    }
                }
                if (!mesaEncontrada)
                {
                    throw new Exception($"LA MESA '{_nMesa}' SE ENCUENTRA EN OTRO ESTADO, NO SE PUDO REALIZAR LA SELECCION");
                }
            }
            else
            {
                throw new Exception($"NO EXISTEN MESAS CON ESTADO '{_estado}'");
            }
            utilities.Delay(4);
        }

        public void SeleccionMozo(string _mozo)
        {
            utilities.ElementExists(_selectMozo);
            if (string.IsNullOrEmpty(_mozo))
            {
                _mozo = "Selecciona un mozo";
            }

            var dropdown = new SelectElement(driver.FindElement(_selectMozo));
            dropdown.SelectByText(_mozo);
            Assert.That(dropdown.SelectedOption.Text, Is.EqualTo(_mozo));
            utilities.Delay(2);
        }

        public void IngresarCodigoItem(string _concepto)
        {
            utilities.SubmitTexto(_codeBarraItem, _concepto);
            utilities.Delay(2);

            By modal = By.CssSelector(".swal-modal");
            By textoError = By.XPath("//div[contains(@class, 'swal-text') and contains(text(), 'No hay un item')]");
            By botonOk = By.CssSelector(".swal-button--confirm");

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

                // Si el modal aparece, se valida el mensaje
                if (wait.Until(ExpectedConditions.ElementIsVisible(modal)).Displayed)
                {
                    var mensaje = driver.FindElement(textoError);
                    if (mensaje.Displayed)
                    {
                        Console.WriteLine("[INFO] No hay un item con este codigo de barra.");
                        driver.FindElement(botonOk).Click();
                        return; // Sí hubo error
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARNING] Ocurrió un error no previsto al verificar el modal: {ex.Message}");
            }
        }

        public void SeleccionItem(string _concepto, string _cantidad)
        {
            if (!string.IsNullOrEmpty(_concepto))
            {
                var dropdown = new SelectElement(driver.FindElement(_selectItem));
                dropdown.SelectByText(_concepto);
                Assert.That(dropdown.SelectedOption.Text, Is.EqualTo(_concepto));
                utilities.Delay(2);
            }

            utilities.InputTexto(_itemCantidad, _cantidad);
            utilities.Delay(2);

            utilities.ClickButton(_agregarItem);
            utilities.Delay(2);
        }

        public void DetalleItem(string _accion, string _item, string _cantidad, string _anotacion)
        {
            // SE UTILIZA AL REALIZAR Y MODIFICAR LA ORDEN
            IWebElement tabla = driver.FindElement(By.Id("acordionAnotacion"));
            IList<IWebElement> filas = tabla.FindElements(By.TagName("tr"));

            foreach (var fila in filas)
            {
                try
                {
                    IWebElement ordenElemento = fila.FindElement(By.CssSelector("td:nth-child(1)"));
                    string orden = ordenElemento.Text;

                    IWebElement descripcionElemento = fila.FindElement(By.CssSelector("td:nth-child(2)"));
                    string descripcion = descripcionElemento.Text;

                    IWebElement cantidadElemento = fila.FindElement(By.CssSelector("td:nth-child(3)"));
                    string cantidad = cantidadElemento.Text;

                    if (descripcion.Contains(_item) && cantidad.Contains(Formato("decimal", _cantidad)))
                    {
                        int ordenNumero = int.Parse(orden); // Convertimos a entero
                        utilities.ScrollViewElement(fila);
                        utilities.Delay(2);
                        switch (_accion)
                        {
                            case "Agregar anotacion":
                                IWebElement botonAnotacion = fila.FindElement(By.CssSelector("a.btn-info"));
                                botonAnotacion.Click();

                                IWebElement inputAnotacion = driver.FindElement(By.Id($"input-anotacion{ordenNumero - 1}"));
                                inputAnotacion.SendKeys(_anotacion);
                                utilities.Delay(2);
                                botonAnotacion.Click();
                                break;
                            case "Eliminar":
                                IWebElement botonEliminar = fila.FindElement(By.CssSelector("button.btn-danger"));
                                botonEliminar.Click();
                                break;
                            default:
                                throw new ArgumentException($"LA ACCION {_accion} NO ES VALIDO");
                        }
                        utilities.Delay(2);
                        utilities.ScrollViewTop();
                        utilities.Delay(4);
                        break; // Sale del bucle al encontrar el botón
                    }
                }
                catch (NoSuchElementException)
                {
                    continue; // Si la fila no tiene una descripción válida, sigue con la siguiente
                }
            }
        }

        public void OpcionOrden(string _opcionOrden, string nOrden)
        {
            By btnOpcion = null;
            switch (_opcionOrden)
            {
                case "guardar":
                    utilities.ClickButton(_guardarOrden);
                    utilities.Delay(4);
                    return;
                case "atender":
                    btnOpcion = _atenderTodo;
                    break;
                case "anular":
                    btnOpcion = _anularTodo;
                    break;
                case "reanudar":
                    btnOpcion = _reanudarOrden;
                    break;
                case "cerrar":
                    btnOpcion = _cerrarOrden;
                    break;
                default:
                    throw new ArgumentException($"La acción '{_opcionOrden}' ingresada no es válida sobre la orden");
            }
            var (tablaEncontrada, cont) = EncontrarOrden(nOrden);
            utilities.Delay(2);
            IWebElement boton = tablaEncontrada.FindElement(btnOpcion); ;
            boton.Click();
            utilities.Delay(4);
        }

        public (IWebElement tablaEncontrada, int contador) EncontrarOrden(string nOrden)
        {
            int contador = 0;

            if (!string.IsNullOrEmpty(nOrden))
            {
                IList<IWebElement> tablas = driver.FindElements(By.XPath("//div[@id='ordenes']//table"));

                foreach (IWebElement tabla in tablas)
                {
                    utilities.ScrollViewElementCentered(tabla);
                    utilities.Delay(2);

                    if (tabla.Text.Contains(nOrden))
                    {
                        return (tabla, contador);
                    }

                    contador += 10;
                }

                throw new Exception($"No se encontró la orden con número {nOrden}");
            }

            // Si no se proporciona un número de orden, tomar la primera tabla encontrada
            IWebElement tablaPrimera = driver.FindElement(By.XPath("//div[@id='ordenes']//table"));
            return (tablaPrimera, contador);
        }// EXCEPCION IMPLEMENTADA


        public string Formato(string accion, string input)
        {
            switch (accion.ToLower())
            {
                case "nombre":
                    // Si el string comienza con números seguidos de '|', lo eliminamos
                    return Regex.IsMatch(input, @"^\d+\|")
                        ? Regex.Replace(input, @"^\d+\|", "").Trim()
                        : input.Trim();

                case "numero":
                    // Expresión regular para capturar solo el número al inicio antes del "|"
                    Match match = Regex.Match(input, @"^(\d+)\|");

                    // Si hay coincidencia, devuelve el número; de lo contrario, devuelve "N/A"
                    return match.Success ? match.Groups[1].Value : "N/A";

                case "decimal":
                    if (decimal.TryParse(input, out decimal numero))
                    {
                        numero.ToString("0.00"); // Convierte a dos decimales
                    }
                    return input;
                default:
                    return "Acción inválida";
            }
        }
    }
}
