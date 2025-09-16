using ExampleSales.Pages.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static readonly By TypeDocumentField = By.XPath("//body/div[@id='wrapper']/div[1]/section[1]/div[1]/div[1]/div[1]/form[1]/div[2]/facturacion-venta[1]/form[1]/div[1]/div[2]/div[1]/div[6]/selector-comprobante[1]/div[1]/ng-form[1]/div[1]/div[1]/span[1]/span[1]/span[1]");
        public static readonly By DebitCardButton = By.Id("labelMedioPago-0-18");
        public static readonly By CashPaymentOption = By.Id("radio1");
        public static readonly By PaymentInformation = By.XPath("//div[@class='box box-primary box-solid']//textarea[@id='informacion']");
        public static readonly By SaveSaleButton = By.XPath("//button[normalize-space()='GUARDAR VENTA']");
        public static readonly By OverlayPath = By.ClassName("block-ui-overlay");


        public void SelectConcept(string codeconcept)
        {
            utilities.ElementExists(txtBarCode);
            Thread.Sleep(4000);
            utilities.SelectOption(selConceptSelection, codeconcept);
            Thread.Sleep(5000);
            Console.WriteLine("✅ El producto fue agregado correctamente.");
        }

        public void InputAmount(string amount)
        {
            utilities.Overlay();
            utilities.EnterField(ConceptAmount, amount);
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
                case "FACTURA":

                    utilities.SelectOption(TypeDocumentField, option);

                    break;

                case "BOLETA":

                    utilities.SelectOption(TypeDocumentField, option);

                    break;

                case "NOTA":

                    utilities.SelectOption(TypeDocumentField, option);
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
            utilities.EnterField(PaymentInformation, information);

        }

        public void SaveSale()
        {
            utilities.ClickButton(SaveSaleButton);
            Thread.Sleep(4000);

        }
    }
}