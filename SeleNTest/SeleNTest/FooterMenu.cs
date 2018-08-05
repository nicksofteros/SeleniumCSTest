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
    class FooterMenu
    {
        private static readonly string logPrefix = "Footer Menu - ";

        private static By MenuPrivacyPolicysSelector = By.LinkText("Privacy Policy");
        private static By MenuCasesSelector = By.XPath("//a[@class='footer__menulink--submenu' and text()='Cases']");
     

        public static void ClickOnNewsAndEvents(bool inNewTab)
        {
            Core.Log(logPrefix + "click on Privacy Policy");

            IWebElement link;

            if(inNewTab)
            {
                Core.OpenLinkInNewTab(MenuPrivacyPolicysSelector);
            }
            else
            {
                link = Core.Driver().FindElement(MenuPrivacyPolicysSelector);
                link.Click();
            }
            
        }

        public static void ClickOnCases(bool inNewTab)
        {
            Core.Log(logPrefix + "click on Cases");

            IWebElement link;

            if (inNewTab)
            {
                Core.OpenLinkInNewTab(MenuCasesSelector);
            }
            else
            {
                Actions action = new Actions(Core.Driver());
 
                link = Core.Driver().FindElement(MenuCasesSelector);
//                action.MoveToElement(link).Perform();
                link.Click();
            }

        }

    }
}
