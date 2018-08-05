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
    class ContactPage
    {
        private static readonly string logPrefix = "Contact Page - ";

        private static By MenuUSWestSelector = By.XPath("//span[contains(.,'U.S. West')]");
        private static By MenuGermanySelector = By.XPath("//span[contains(.,'Germany')]");

        public static void ClickOnUSWest()
        {
            Core.Log(logPrefix + "click on US West");

            IWebElement menu = Core.Driver().FindElement(MenuUSWestSelector);
            menu.Click();
            Assert.IsTrue(menu.GetAttribute("class").Contains("selected"));
        }

        public static void moveMouseToGermanyMenu()
        {
            Core.Log(logPrefix + "move mouse to Gemrany menu");
            IWebElement menu = Core.Driver().FindElement(MenuUSWestSelector);

            Actions action = new Actions(Core.Driver());
            action.MoveToElement(menu).Perform();
        }

    }
}
