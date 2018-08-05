using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleNTest
{
    static class Core
    {
        private static IWebDriver driver;
        private static readonly string logPrefix = "[Core] - ";
        private static readonly log4net.ILog log4Net = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string Browser="";

        public static IWebDriver Driver()
        {
            return driver;
        }

        public static void StartDriver(string whichDriver)
        {
            Log(logPrefix + "starting driver " + whichDriver);
            Browser = whichDriver;

            switch (whichDriver)
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;

                //there is some problem with compabitility driver and firefox version
                //both should be compatible to avoid blank page after firefox started
                //Current version of driver (0.21.0) supports FF 57 or higher
                case "firefox":

                    FirefoxOptions options = new FirefoxOptions();
                    options.AddAdditionalCapability("moz:webdriverClick", true, true);

                    //firefox preferences to avoid download manager window (unfortunetly it doesn't work):
                    options.SetPreference("browser.download.folderList", 1);
                    options.SetPreference("browser.download.useDownloadDir", true);
                    options.SetPreference("browser.download.manager.showWhenStarting", false);
                    options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");
                    options.SetPreference("pdfjs.disabled", true);
                    options.SetPreference("browser.download.panel.shown", false);
                    options.SetPreference("browser.download.manager.alertOnEXEOpen", false);
                    options.SetPreference("browser.download.manager.closeWhenDone", true);
                    options.SetPreference("browser.download.manager.focusWhenStarting", false);
                    options.SetPreference("browser.download.manager.showWhenStarting", false);
                    options.SetPreference("browser.download.manager.useWindow", false);
                    options.SetPreference("browser.download.manager.showAlertOnComplete", false);
                    options.SetPreference("pdfjs.enabledCache.state", false);
                    options.SetPreference("browser.helperApps.alwaysAsk.force", false);
                    options.SetPreference("plugin.scan.Acrobat", "99.0");
                    options.SetPreference("plugin.scan.plid.all", false);

                    driver = new FirefoxDriver(options);

                    driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(10));
                    driver.Manage().Timeouts().AsynchronousJavaScript.Add(TimeSpan.FromSeconds(10));

                    break;

                default:
                    {
                        Log(logPrefix + "Not compatible drivers");
                        break;
                    }
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            
            Log(logPrefix + "driver " + whichDriver + " started");
        }

        public static void CloseDriver()
        {
            Log(logPrefix + "closing driver");

            driver.Close();
            driver.Quit();

            Log(logPrefix + "driver closed");
        }

        public static void Log(string text)
        {
            //You can choose any logger you want
            //Console.WriteLine(text);  //build in logger
            log4Net.Info(text);         //log4Net logger
        }

        public static Screenshot TakeScreenshot()
        {
            Log(logPrefix + "taking a screenshot");
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();

            return ss;
        }

        public static string TakeScreenshotBase64()
        {
            return TakeScreenshot().AsBase64EncodedString;
        }

        public static void OpenLinkInNewTab(By by)
        {
            Log(logPrefix + "open link in new tab");

            IWebElement element = driver.FindElement(by);
            
            Actions action = new Actions(driver);
            //trick to scroll to realy end of page
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

            action.MoveToElement(element).Perform();

            //This is the only way that works for Chrome:
            action.KeyDown(Keys.Control).MoveToElement(element).Click().Perform();
        }

        public static void SwitchToLastTab()
        {
            Log(logPrefix + "switch to last tab");
            Thread.Sleep(1000);     //change it later to something better
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public static void SwitchToFirstTab()
        {
            Log(logPrefix + "switch to first tab");
            Thread.Sleep(1000);     //change it later to something better
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        public static void CheckFileDownloaded(string filename)
        {
            Log(logPrefix + "check downloaded file: " + filename);

            bool exist = false;
            int waitMilis = 0;
            int waitperiod = 500;

            //check if file exists ind Downloads directory in each 0.5 seconds
            //after 10 seconds quit with false result
            do
            {
                Thread.Sleep(waitperiod);
                waitMilis += waitperiod;
                exist = CheckFileExistsFromDownloadsDirectory(filename);
            }
            while (!exist && waitMilis <= 10000);

            Assert.IsTrue(exist);
        }

        private static bool CheckFileExistsFromDownloadsDirectory(string filename)
        {
            bool exist = false;
            string Path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
            string[] filePaths = Directory.GetFiles(Path);

            foreach (string p in filePaths)
            {
                if (p.Contains(filename))
                {
                    FileInfo thisFile = new FileInfo(p);

                    if (thisFile.Exists && thisFile.Length > 0)
                    {
                        exist = true;
                        //delete file 
                        File.Delete(p);
                    }
                    break;
                }
            }

            return exist;
        }
    }
}
