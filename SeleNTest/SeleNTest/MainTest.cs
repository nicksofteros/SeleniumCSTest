using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleNTest
{
    public class MainTest
    {
        [Test]
        public void OmadaArticlesTestChrome()
        {
            //choose driver for the test
            Core.StartDriver("chrome");

            //run real test
            OmadaArticlesTest();
        }

        [Test]
        public void OmadaContactTestChrome()
        {
            //choose driver for the test
            Core.StartDriver("chrome");

            //run real test
            OmadaContactTest();
        }

        [Test]
        public void OmadaPolicyTestChrome()
        {
            //choose driver for the test
            Core.StartDriver("chrome");

            //run real test
            OmadaPolicyTest();
        }

        [Test]
        public void OmadaCasesTestChrome()
        {
            //choose driver for the test
            Core.StartDriver("chrome");

            //run real test
            OmadaCasesTest();
        }

        [Test]
        public void OmadaArticlesTestFirefox()
        {
            //choose driver for the test
            Core.StartDriver("firefox");

            //run real test
            OmadaArticlesTest();
        }

        [Test]
        public void OmadaContactTestFirefox()
        {
            //choose driver for the test
            Core.StartDriver("firefox");

            //run real test
            OmadaContactTest();
        }

        [Test]
        public void OmadaPolicyTestFirefox()
        {
            //choose driver for the test
            Core.StartDriver("firefox");

            //run real test
            OmadaPolicyTest();
        }

        [Test]
        public void OmadaCasesTestFirefox()
        {
            //choose driver for the test
            Core.StartDriver("firefox");

            //run real test
            OmadaCasesTest();
        }


        public void OmadaArticlesTest()
        {
            GoToMainURL();
            MainPage.CheckMainPageElements();
            MainPage.CloseCookieBar();

            MainPage.HeaderSearchText("gartner");
            SearchResultsPage.CheckResultsCountGreaterThan(1);
            //NOTICE: I changed text to check, because previous text was not present in results
            SearchResultsPage.CheckTextInResults("Omada Recognized by Gartner for Critical Capabilities in IGA");
            SearchResultsPage.ClickOnLinkResult("Gartner IAM Summit 2016 - London");
            ArticlePage.CheckArticle("Gartner IAM Summit 2016 - London", "Omada is a sponser at the Gartner IAM Summit 2016 in London, UK.");

            //check article on news list
            TopMenu.ClickOnNewsAndEvents();
            NewsAndEventsPage.ClickOnNews();
            NewsListPage.checkIfNewsIsPresent("Gartner IAM Summit 2016 - London");
        }

        public void OmadaContactTest()
        {
            GoToMainURL();

            TopMenu.ClickOnHeaderLogo();
            TopMenu.ClickOnContact();
            ContactPage.ClickOnUSWest();

            string beforMove = Core.TakeScreenshotBase64();
            ContactPage.moveMouseToGermanyMenu();
            string afterMove = Core.TakeScreenshotBase64();

            //check screen shot before and after move mouse on menu
            Assert.IsFalse(beforMove.Equals(afterMove));
        }

        public void OmadaPolicyTest()
        {
            GoToMainURL();

            FooterMenu.ClickOnNewsAndEvents(true);

            Core.SwitchToLastTab();
            PrivacyPolicyPage.CheckPageElements();

            Core.SwitchToFirstTab();
            MainPage.CloseCookieBar();

            //close last tab:
            Core.SwitchToLastTab();
            Core.Driver().Close();

            Core.SwitchToFirstTab();
            //check if the policy bar is not displayed:
            MainPage.checkIfPolicyBarDisplayed(false);
        }

        public void OmadaCasesTest()
        {
            GoToMainURL();

            MainPage.CloseCookieBar();
            FooterMenu.ClickOnCases(false);
            CasesPage.ClickOnDownloadEccoPDF();
            DownloadPDFPage.fillForm("Prezes", 
                                     "Wiesław", 
                                     "Wszywka",
                                     "Wieslaw@gmail.pl",
                                     "+48 22 666 00 77",
                                     "Testerzy S.A.",
                                     "Poland");
            DownloadPDFPage.ClickDownloadPDF();
            DownloadPDFPage.ClickFinalDownloadLink("ECCO");
            Core.CheckFileDownloaded("Omada-Case-ECCO-Shoes.pdf");
        }

        private void GoToMainURL()
        {
            Core.Driver().Navigate().GoToUrl("https://www.omada.net");
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                Core.CloseDriver();
            }
            catch (Exception)
            {
                Core.Log("unable to close test browser");
            }
        }
    }
}
