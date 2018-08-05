using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleNTest
{
    class SearchResultsPage
    {
        private static readonly string logPrefix = "Search Results Page - ";

        private static IList<IWebElement> SearchResultsList = Core.Driver().FindElements(By.XPath("//section[@class='search-results__item']/a"));


        public static void CheckTextInResults(string text)
        {
            Core.Log(logPrefix + "checking spesific text in results");
            bool isTextPresent = false;

            foreach (IWebElement element in SearchResultsList)
            {
                //uncomment to see all links from results
                //Core.Log(logPrefix + element.Text);
                if (element.Text.Contains(text))
                {
                    isTextPresent = true;
                }

            }

            Assert.IsTrue(isTextPresent);
        }

        public static int GetResultsCount()
        {
            return SearchResultsList.Count;
        }

        public static void CheckResultsCount(int number)
        {
            Core.Log(logPrefix + "checking results count");
            Assert.AreEqual(GetResultsCount(), number);
        }

        public static void CheckResultsCountGreaterThan(int number)
        {
            Core.Log(logPrefix + "checking results count");
            Assert.Greater(GetResultsCount(), number);
        }

        public static void ClickOnLinkResult(string linkText)
        {
            Core.Log(logPrefix + "click on spesific link from results");
            IWebElement linkElement = Core.Driver().FindElement(By.XPath("//section[@class='search-results__item']/a[contains(text(),'"+ linkText + "')]"));
            linkElement.Click();
        }
    }
}
