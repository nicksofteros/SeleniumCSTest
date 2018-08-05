using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleNTest
{
    class TopMenu
    {
        private static readonly string logPrefix = "Top Menu - ";

        private static By MenuNewsAndEventsSelector = By.LinkText("News & Events");
        private static By MenuContactSelector = By.LinkText("Contact");
        private static By MenuMainPage = By.ClassName("header__logo");
        
        public static void ClickOnNewsAndEvents()
        {
            Core.Log(logPrefix + "click on News & Event");

            IWebElement menu = Core.Driver().FindElement(MenuNewsAndEventsSelector);
            menu.Click();
        }

        public static void ClickOnHeaderLogo()
        {
            Core.Log(logPrefix + "click on Header logo");

            IWebElement menu = Core.Driver().FindElement(MenuMainPage);
            menu.Click();
        }

        public static void ClickOnContact()
        {
            Core.Log(logPrefix + "click on Contact link");

            IWebElement menu = Core.Driver().FindElement(MenuContactSelector);
            menu.Click();
        }
    }
}
