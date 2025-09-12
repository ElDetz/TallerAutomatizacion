using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace ExampleLogin.Reporting
{
    public static class ExtentReport
    {
        private static readonly object _lock = new();
        private static ExtentReports _extent;

        public static string RunFolder { get; private set; }
        public static string ScreenshotsFolder { get; private set; }

        public static ExtentReports Instance
        {
            get
            {
                if (_extent == null)
                {
                    lock (_lock)
                    {
                        if (_extent == null)
                        {
                            var baseReports = Path.Combine(NUnit.Framework.TestContext.CurrentContext.WorkDirectory, "Reports");
                            Directory.CreateDirectory(baseReports);

                            RunFolder = Path.Combine(baseReports, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                            Directory.CreateDirectory(RunFolder);

                            ScreenshotsFolder = Path.Combine(RunFolder, "screenshots");
                            Directory.CreateDirectory(ScreenshotsFolder);

                            var reportPath = Path.Combine(RunFolder, "ExtentReport.html");
                            var spark = new ExtentSparkReporter(reportPath);
                            spark.Config.DocumentTitle = "Reporte de Automatización";
                            spark.Config.ReportName = "Suite Selenium .NET (sin POM)";

                            _extent = new ExtentReports();
                            _extent.AttachReporter(spark);
                            _extent.AddSystemInfo("SO", Environment.OSVersion.ToString());
                            _extent.AddSystemInfo("Navegador", "Chrome");
                        }
                    }
                }
                return _extent;
            }
        }

        public static void Flush() => _extent?.Flush();
    }
}
