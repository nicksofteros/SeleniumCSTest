using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace SeleNTest
{
    class PrivacyPolicyPage
    {
        private static readonly string logPrefix = "Privacy Policy Page - ";

        private static By TextHeadingSelector = By.XPath("//h1[@class='text__heading']");
        private static string headerText = "WEBSITE PRIVACY POLICY";
        private static string TitleText = "Omada | Processing of Personal Data";

        public static void CheckPageElements()
        {
            Core.Log(logPrefix + "checking page elements");

            IWebElement header = Core.Driver().FindElement(TextHeadingSelector);

            Assert.AreEqual(headerText, header.Text);
            Assert.AreEqual(TitleText, Core.Driver().Title);
        }


    }
}
