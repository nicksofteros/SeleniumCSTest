using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleNTest
{
    class NewsListPage
    {
        private static readonly string logPrefix = "News list - ";

        public static void checkIfNewsIsPresent(string newsTitle)
        {
            Core.Log(logPrefix + "checking if news is present on list: "+ newsTitle);

            IWebElement news = Core.Driver().FindElement(By.XPath("//h1[@class='cases__heading' and contains(.,'"+ newsTitle + "')]") );
            Assert.IsTrue(news.Displayed);
        }
    }
}
