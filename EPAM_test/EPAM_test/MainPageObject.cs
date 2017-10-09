using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;
using System.Text;
using log4net.Repository.Hierarchy;
using OpenQA.Selenium.Support.UI;
using Assert = NUnit.Framework.Assert;

namespace EPAM_test
{
    public class MainPageObject: BaseTest
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;
        private readonly WebDriverWait _wait;

        public MainPageObject(IWebDriver browser)
        {
            this._driver = browser;
            this._driver.Manage().Window.Maximize();
            this._baseUrl = ConfigurationManager.AppSettings["URL"];
            this._wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
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
            _driver.Navigate().GoToUrl(_baseUrl);
        }

        public void Search(string textToType)
        {
            SearchBox.Clear();
            SearchBox.SendKeys(textToType);
            SearchBox.SendKeys(Keys.Tab);
            GoButton.Click();
        }

        public void AssertTextResults(string expectedText)
        {
            Assert.IsTrue(ResultsCountDiv.Text.Contains(expectedText), "The \""+ expectedText + "\" not Found!");
        }

        public void VerifyTextResults(string expectedText)
        {
            VerifyTextResults(expectedText, 0);
        }

        public void VerifyTextResults(string expectedText, int iteration)
        {
            if (iteration == 0)
            {
                VerificationErrors.Append($"The \"{expectedText}\" not Found!");
            }
            else
            {
                if (!ResultsCountDiv.Text.Contains(expectedText))
                {
                    VerificationErrors.Append($"The \"{expectedText}\" on the page N{iteration} not Found!\r\n");
                }
            }
        }

        public void WaitAndClickElement(By by)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(by)).Click();
        }

        public string GetTitle()
        {
            var title = _driver.Title.ToLower();
            return title;
        }

        public string GetUrl()
        {
            var url = _driver.Url;
            return url;
        }

        public void Quit()
        {
            _driver.Quit();
        }
        
    }
}