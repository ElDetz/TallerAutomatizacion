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
            // utilities = new Utilities(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }



    }
}