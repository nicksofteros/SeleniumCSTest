using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleNTest
{
    class MainPage
    {
        //string variables
        private static readonly string TitleText = "Identity Management | Omada Identity";
        private static readonly string HeadlineHeadingText = "Omada is a Global Market-Leading Provider of Innovative Solutions and Services for Identity Management and Access Governance.";
        private static readonly string logPrefix = "Main Page - ";
        private static readonly string MainURL = "https://www.omada.net";

        //selectors
        private static By HeadlineHeadingSelector = By.ClassName("headline__heading");
        private static By HeaderSearchInputSelector = By.XPath("//form[@class='header__search']/input");
        private static By CookieBarSelector = By.XPath("//div[@class='cookiebar__container']");
        private static By CookieBarCloseSelector = By.ClassName("cookiebar__button");

        //elements
        private static IWebElement HeadlineHeadingElement = Core.Driver().FindElement(HeadlineHeadingSelector);
        private static IWebElement HeaderSearchElement = Core.Driver().FindElement(HeaderSearchInputSelector);

        public static void GoToMainPage()
        {
            Core.Log(logPrefix + "go to main page");
            Core.Driver().Navigate().GoToUrl(MainURL);
        }

        //check if main page contains required elements
        public static void CheckMainPageElements()
        {
            Core.Log(logPrefix + "checking main page elements");

            Assert.AreEqual(TitleText, Core.Driver().Title);
            Assert.AreEqual(HeadlineHeadingText, HeadlineHeadingElement.Text);
            Assert.AreEqual(HeadlineHeadingElement.Displayed, true);

            Core.Log(logPrefix + "elements checked");
        }

        public static void HeaderSearchText(string text)
        {
            Core.Log(logPrefix + "searching for text on the top");

            HeaderSearchElement.Click();
            HeaderSearchElement.Clear();
            HeaderSearchElement.SendKeys(text);
            HeaderSearchElement.SendKeys(Keys.Enter);
        }

        public static void CloseCookieBar()
        {
            Core.Log(logPrefix + "closing cookie bar");
            IWebElement CookieBarCloseElement = Core.Driver().FindElement(CookieBarCloseSelector);

            CookieBarCloseElement.Click();
        }

        public static void checkIfPolicyBarDisplayed(bool shouldBeDisplayed)
        {
            IWebElement cookieBarElement = Core.Driver().FindElement(CookieBarSelector);

            if(shouldBeDisplayed)
            {
                Assert.IsTrue(cookieBarElement.Displayed);
            }
            else
            {
                Assert.IsFalse(cookieBarElement.Displayed);
            }
        }
    }
}
