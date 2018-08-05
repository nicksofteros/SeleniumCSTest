using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SeleNTest
{
    class NewsAndEventsPage
    {
        private static readonly string logPrefix = "News & Events Page - ";

        private static By NewsLinkSelector = By.XPath("//a[@class='spots__button' and text()='News']");


        public static void ClickOnNews()
        {
            Core.Log(logPrefix + "click on News");


           // IJavaScriptExecutor js = (IJavaScriptExecutor)Core.Driver();

            //This will scroll the web page till end.		
            //js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight-100)");

            IWebElement news = Core.Driver().FindElement(NewsLinkSelector);
            news.Click();
        }
    }
}
