using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SeleNTest
{
    class DownloadPDFPage
    {
        private static readonly string logPrefix = "Download PDF Page - ";

        private static By JobTitleSelector       = By.XPath("//input[@leadfield='jobtitle']");
        private static By FirstNameSelector      = By.XPath("//input[@leadfield='firstname']");
        private static By LastNameSelector       = By.XPath("//input[@leadfield='lastname']");
        private static By EmailSelector          = By.XPath("//input[@leadfield='emailaddress1']");
        private static By TelephoneSelector      = By.XPath("//input[@leadfield='telephone1']");
        private static By CompanyNameSelector    = By.XPath("//input[@leadfield='companyname']");
        private static By AddressSelector        = By.XPath("//select[@leadfield='address1_county']");
        private static By AcceptChecboxSelector  = By.XPath("//input[@leadfield='new_omada_buddymail']");
        private static By SliderSelector         = By.Id("Slider");
        private static By DownloadSelector       = By.XPath("//input[@value='Download PDF']");

        public static void fillForm(string jobtitleParam, 
                                    string firstNameParam, 
                                    string lastNameParam,
                                    string emailParam,
                                    string telephoneParam,
                                    string companyNameParam,
                                    string cuntryParam)
        {
            Core.Log(logPrefix + "fill all form inputs");

            IWebElement jobtitleElement     = Core.Driver().FindElement(JobTitleSelector);
            IWebElement firstnameElement    = Core.Driver().FindElement(FirstNameSelector);
            IWebElement lastnameElement     = Core.Driver().FindElement(LastNameSelector);
            IWebElement emailElement        = Core.Driver().FindElement(EmailSelector);
            IWebElement telephoneElement    = Core.Driver().FindElement(TelephoneSelector);
            IWebElement companynameElement  = Core.Driver().FindElement(CompanyNameSelector);
            IWebElement addressElement      = Core.Driver().FindElement(AddressSelector);
            IWebElement acceptElement       = Core.Driver().FindElement(AcceptChecboxSelector);
            IWebElement sliderElement       = Core.Driver().FindElement(SliderSelector);
            SelectElement countrySelectElement = new SelectElement(addressElement);

            jobtitleElement.SendKeys(jobtitleParam);
            firstnameElement.SendKeys(firstNameParam);
            lastnameElement.SendKeys(lastNameParam);
            emailElement.SendKeys(emailParam);
            telephoneElement.SendKeys(telephoneParam);
            companynameElement.SendKeys(companyNameParam);
            countrySelectElement.SelectByText(cuntryParam);
            acceptElement.Click();

            Actions action = new Actions(Core.Driver());

            Core.Log(logPrefix + "size: " + Core.Driver().Manage().Window.Size);

            //scroll down to see slider element - fix for firefox issue
            //with outofbounds during scroll down to the element
            acceptElement.SendKeys(Keys.PageDown);

            //anothert fix for firefox - for slider element
            if (Core.Browser.Equals("firefox"))
            {
                MoveSliderToPercent(sliderElement, 77);
            }
                

            //slider - ATTENTION!! - don't move the mouse during this action :)
            new Actions(Core.Driver())
                .DragAndDropToOffset(sliderElement, 154, 0)
                .Build()
                .Perform();
        }

        public static void MoveSliderToPercent(IWebElement slider, int percent)
        {
            Actions builder = new Actions(Core.Driver());

            int height = slider.Size.Height;
            int width = slider.Size.Width;

            builder.ClickAndHold(slider).MoveByOffset(0, -(height / 2)).
                                MoveByOffset((int)((width / 100) * percent), 0).
                                Release().Build().Perform();
        }

        public static void ClickDownloadPDF()
        {
            Core.Log(logPrefix + "download the file");
            IWebElement downloadElement = Core.Driver().FindElement(DownloadSelector);
            downloadElement.Click();
        }

        public static void ClickFinalDownloadLink(string company)
        {
            Core.Log(logPrefix + "finally download the PDF file, link: "+ "   Download Customer Case: " + company);
            IWebElement linkElement = Core.Driver().FindElement(By.LinkText("Download Customer Case: "+ company));
            linkElement.Click();

            //fix for firefox download
            if(Core.Browser.Equals("firefox"))
            {
                System.Windows.Forms.SendKeys.SendWait("{Down}");
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }
            
        }

    }
}
