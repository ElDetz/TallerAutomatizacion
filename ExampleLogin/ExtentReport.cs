using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System;
using System.IO;

namespace ExampleLogin
{
    public class ExtentReport
    {
        public static ExtentReports _extentReport;
        public static ExtentTest _test;

        public static string dir = AppDomain.CurrentDomain.BaseDirectory;
        public static string testResultPath = Path.Combine(dir, "bin\\Debug\\net8.0\\TestResults");

        // Inicialización del reporte
        public static void ExtentReportInit()
        {
            try
            {
                // Crear directorio TestResults si no existe
                if (!Directory.Exists(testResultPath))
                {
                    Directory.CreateDirectory(testResultPath);
                    Console.WriteLine("Directory 'TestResults' created.");
                }

                // Crear y configurar el reporte HTML
                var htmlReporter = new ExtentHtmlReporter(testResultPath);
                htmlReporter.Config.ReportName = "Automation Status Report";
                htmlReporter.Config.DocumentTitle = "Automation Status Report";
                htmlReporter.Config.Theme = Theme.Standard;
                _extentReport = new ExtentReports();
                _extentReport.AttachReporter(htmlReporter);
                _extentReport.AddSystemInfo("Application", "SIGES-Restaurante");
                _extentReport.AddSystemInfo("Browser", "Chrome");
                _extentReport.AddSystemInfo("OS", "Windows");

                Console.WriteLine("ExtentReports initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during ExtentReport initialization: " + ex.Message);
            }
        }

        // Finalización del reporte
        public static void ExtentReportTearDown()
        {
            try
            {
                // Guardar el reporte final
                _extentReport.Flush();
                Console.WriteLine("ExtentReports flushed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during flushing the report: " + ex.Message);
            }
        }

        // Método para crear el reporte de la prueba
        public static void StartTest(string testName)
        {
            try
            {
                _test = _extentReport.CreateTest(testName);
                Console.WriteLine($"Started test: {testName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during test creation: " + ex.Message);
            }
        }

        // Método para agregar captura de pantalla
        public static string AddScreenshot(IWebDriver driver)
        {
            try
            {
                ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
                Screenshot screenshot = takesScreenshot.GetScreenshot();

                // Crear nombre único para la captura de pantalla
                string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string screenshotLocation = Path.Combine(testResultPath, fileName);
                screenshot.SaveAsFile(screenshotLocation);

                Console.WriteLine($"Screenshot saved at: {screenshotLocation}");
                return screenshotLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during screenshot capture: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
