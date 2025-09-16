using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System;
using System.IO;

namespace ExampleSales.Utility
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        public static ExtentTest _feature;
        public static ExtentTest _scenario;

        // Carpeta raíz de resultados
        public static string dir = AppDomain.CurrentDomain.BaseDirectory;
        public static string testResultRoot = dir.Replace("bin\\Debug\\net8.0", "TestResults");

        // Variables para esta ejecución
        public static string runTimestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public static string runFolder = Path.Combine(testResultRoot, runTimestamp);
        public static string screenshotsFolder = Path.Combine(runFolder, "Screenshots");

        public static void ExtentReportInit()
        {
            // Crear carpetas de la ejecución
            Directory.CreateDirectory(runFolder);
            Directory.CreateDirectory(screenshotsFolder);

            // Reporte con nombre único
            string reportFile = Path.Combine(runFolder, $"ExtentReport_{runTimestamp}.html");

            var htmlReporter = new ExtentHtmlReporter(reportFile);
            htmlReporter.Config.ReportName = "Automation Status Report";
            htmlReporter.Config.DocumentTitle = "Automation Status Report";
            htmlReporter.Config.Theme = Theme.Standard;

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Application", "ExampleSales");
            _extentReports.AddSystemInfo("Browser", "Chrome");
            _extentReports.AddSystemInfo("OS", "Windows");
        }

        public static void ExtentReportTearDown()
        {
            _extentReports?.Flush();
        }

        public string addScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();

            // Nombre único de screenshot
            string fileName = $"{SanitizeFileName(scenarioContext.ScenarioInfo.Title)}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            string screenshotLocation = Path.Combine(screenshotsFolder, fileName);
            screenshot.SaveAsFile(screenshotLocation);
            return screenshotLocation;
        }

        // Quitar caracteres inválidos para el nombre de archivo
        private static string SanitizeFileName(string input)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                input = input.Replace(c, '_');
            return input;
        }
    }
}
