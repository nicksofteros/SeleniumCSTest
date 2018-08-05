using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleNTest
{
    class CasesPage
    {
        private static readonly string logPrefix = "Article details Page - ";

        private static By EccoDwonloiadPDFSelector = By.XPath("//a[contains(@href,'ecco-case')]");
                                            

        public static void ClickOnDownloadEccoPDF()
        {
            Core.Log(logPrefix + "click on download PDF for ECCO");

            IWebElement downloadPDF = Core.Driver().FindElement(EccoDwonloiadPDFSelector);
            downloadPDF.Click();

        }
    }
}
