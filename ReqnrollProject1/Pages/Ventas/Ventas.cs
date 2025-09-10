using ExampleSales.Pages.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace ExampleSales.Pages.Ventas
{
    public class VentasPage
    {

        private IWebDriver driver;
        Utilities utilities;
        WebDriverWait wait;

        public VentasPage(IWebDriver driver, int timeoutInSeconds = 30)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "El WebDriver no puede ser null.");
            }
            this.driver = driver;
            utilities = new Utilities(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public static readonly By txtBarCode = By.Id("idCodigoBarra");
        public static readonly By selConceptSelection = By.XPath("//body/div[@id='wrapper']/div[1]/section[1]/div[1]/div[1]/div[1]/form[1]/div[1]/div[1]/div[1]/registrador-detalles[1]/div[1]/div[1]/selector-concepto-comercial[1]/ng-form[1]/div[1]/div[3]/div[1]/div[1]/span[1]/span[1]/span[1]");
        public static readonly By ConceptAmount = By.Id("cantidad-0");
        public static readonly By ConceptPrice = By.Id("precio-0");
        public static readonly By IgvActive = By.Id("ventaigv0");
        public static readonly By IdCustomer = By.Id("DocumentoIdentidad");
        public static readonly By TypeDocumentField = By.CssSelector("#select2-TipoComprobante-fq-container");
        public static readonly By DebitCardButton = By.Id("labelMedioPago-0-18");
        public static readonly By CashPaymentOption = By.Id("radio1");
        public static readonly By PaymentInformation = By.Id("informacion");
        public static readonly By SaveSaleButton = By.XPath("//button[normalize-space()='GUARDAR VENTA']");
        public static readonly By OverlayPath = By.ClassName("block-ui-overlay");


        public void SelectConcept(string codeconcept)
        {
            utilities.EnterField(txtBarCode, codeconcept);
        }

        public void TypeSelectConcept(string option, string value)
        {
            option = option.ToUpper();
            utilities.ElementExists(txtBarCode);
            switch (option)
            {
                case "BARRA":
                    utilities.WaitExistsVisible(txtBarCode, OverlayPath);
                    utilities.EnterField(txtBarCode, value);
                    Thread.Sleep(4000);
                    Console.WriteLine("✅ El producto fue agregado correctamente.");
                    break;

                case "SELECCION":
                    Thread.Sleep(4000);
                    utilities.SelectOption(selConceptSelection, value);
                    Thread.Sleep(5000);
                    Console.WriteLine("✅ El producto fue agregado correctamente.");
                    break;

                default:
                    throw new ArgumentException($"La opción {option} no es válido");
            }
        }

        public void InputAmountAndPrice(string amount, string price)
        {
            utilities.Overlay();

            utilities.EnterField(ConceptAmount, amount);

            utilities.EnterField(ConceptPrice, price);
        }

        public void SelectIGV(string option)
        {
            switch (option)
            {
                case "Si":
                    utilities.ClickButton(IgvActive);
                    break;
            }
        }

        public void EnterCustomer(string dni)
        {

            utilities.Overlay();

            utilities.EnterField(IdCustomer, dni);
        }

        public void SelectTypeDocument(string option)
        {
            switch (option)
            {
                case "FACTURA ELECTRONICA":

                    utilities.SelectComboBox(TypeDocumentField, option);

                    break;

                case "BOLETA DE VENTA ELECTRONICA":

                    utilities.SelectComboBox(TypeDocumentField, option);

                    break;

                case "NOTA DE COMPRA (INTERNA)":

                    utilities.SelectComboBox(TypeDocumentField, option);
                    break;

                default:
                    throw new ArgumentException($"La opción {option} no es válido");
            }

        }

        public void SelectPaymentType(string option)
        {

            switch (option)
            {
                case "Contado":
                    utilities.ClickButton(CashPaymentOption);
                    break;

                case "Crédito Rápido":
                    //utilities.ClickButton(QuickPaymentOption);
                    break;

                case "Crédito Configurado":
                    //utilities.ClickButton(ConfiguredPaymentOption);
                    break;

                default:
                    throw new ArgumentException($"La opción '{option}' no es válido");
            }
        }

        public void PaymentMethod(string option)
        {
            option = option.ToUpper();

            switch (option)
            {
                case "DEPCU":

                    //utilities.ClickButton(DepositButton, option);
                    break;

                case "TRANFON":

                    //utilities.ClickButton(TransferButton, option);
                    break;

                case "TDEB":

                    utilities.ClickButton(DebitCardButton);
                    break;

                case "TCRE":

                    //utilities.ClickButton(CreditCardButton, option);
                    break;

                case "EF":

                    //utilities.PaymentMethodUtility(PaymentMethodPath.CashButton, option);
                    break;

                case "PTS":

                    //utilities.PaymentMethodUtility(PaymentMethodPath.PointsButton, option);
                    break;

                default:
                    throw new ArgumentException($"La opción {option} no es válido");
            }
            Thread.Sleep(4000);
        }
        public void InformationPayment(string information)
        {
            utilities.Overlay();

            utilities.EnterField(PaymentInformation, information);

        }

        public void SaveSale()
        {
            utilities.ClickButton(SaveSaleButton);

        }
    }
}