using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;
using log4net;
using log4net.Config;
using Assert = NUnit.Framework.Assert;

namespace EPAM_test
{
    public class MainPageObject
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainPageObject));

        private readonly IWebDriver _driver;
        private readonly string _baseUrl;

        public MainPageObject(IWebDriver browser)
        {
            this._driver = browser;
            this._driver.Manage().Window.Maximize();
            this._baseUrl = ConfigurationManager.AppSettings["URL"];
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "lst-ib")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.Name, Using = "btnK")]
        public IWebElement GoButton { get; set; }

        [FindsBy(How = How.Id, Using = "search")]
        public IWebElement ResultsCountDiv { get; set; }

        public void Navigate()
        {
            this._driver.Navigate().GoToUrl(this._baseUrl);
            logger.Debug("Go to " + _baseUrl);
        }

        public void Search(string textToType)
        {
            this.SearchBox.Clear();
            logger.Debug("Clear Search field");

            this.SearchBox.SendKeys(textToType);
            logger.Debug("SendKeys: " + textToType);

            this.SearchBox.SendKeys(Keys.Tab);

            this.GoButton.Click();
            logger.Debug("Click Search");
        }

        public void ValidateResults(string expectedText)
        {
            Assert.IsTrue(this.ResultsCountDiv.Text.Contains(expectedText));
        }
    }
}