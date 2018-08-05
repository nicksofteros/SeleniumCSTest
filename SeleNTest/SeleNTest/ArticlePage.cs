using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleNTest
{
    class ArticlePage
    {
        private static readonly string logPrefix = "Article details Page - ";

        private static By ArticleHeaderSelector = By.ClassName("text__heading");
 
        
        public static void CheckArticle(string header, string title)
        {
            Core.Log(logPrefix + "click on spesific link from results");

            IWebElement headerElement = Core.Driver().FindElement(ArticleHeaderSelector);
            IWebElement textElement = Core.Driver().FindElement(ArticleHeaderSelector);

            Assert.AreEqual(header, headerElement.Text);
            Assert.AreEqual(title, Core.Driver().Title);
        }
    }
}
