using System;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading;
using log4net;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace EPAM_test
{
    [TestFixture]
    public class Tests
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Tests));

        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private WebDriverWait _wait;
        private MainPageObject _googleMainPageObject;

        [SetUp]
        public void SetupTest()
        {
            _driver = new InternetExplorerDriver();
            _verificationErrors = new StringBuilder();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _googleMainPageObject = new MainPageObject(_driver);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", _verificationErrors.ToString());
        }

        [Test]
        public void Test1()
        {
            logger.Info("Start Test1");

            var request = "automation";

            // Open IE and Navigate to google.com.ua 
            _googleMainPageObject.Navigate();

            //Search for “automation”.
            _googleMainPageObject.Search(request);

            //Waiting for Search result appearing
            _wait.Until(ExpectedConditions.ElementExists(By.Id("vs0p1c0")));

            //Open the first link on search results page.
            _driver.FindElement(By.Id("vs0p1c0")).Click();

            logger.Debug("Click on the first link in Search result");

            //Verify that title contains searched word
            try
            {
                Assert.IsTrue(_driver.Title.ToLower().Contains(request));
                logger.Info("Test1 is PASSED.");
            }
            catch (Exception e)
            {
                logger.Debug("Test1 is FAILED! Title doesn't contain title " + request + " Error: " + e.Message + DateTime.Now);
            }
            
        }

        [Test]
        public void Test2()
        {
            logger.Info("Start Test2");

            var request = "automation";
            var expectedText = "testautomationday.com";

            // Open IE and Navigate to google.com.ua 
            _googleMainPageObject.Navigate();

            //Search for “automation”.
            _googleMainPageObject.Search(request);

            //Searching "testautomationday.com" in search results of 1, 2, 3, 4, 5 pages
            logger.Info("Check page #1");
            try
            {
                _googleMainPageObject.ValidateResults(expectedText);
                logger.Debug("True");
            }
            catch (AssertionException e)
            {
                _verificationErrors.Append("1 page: " + e.Message);
                logger.Debug("False");
            }

            logger.Info("Check page #2");
            _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("2")));
            _driver.FindElement(By.LinkText("2")).Click();
            try
            {
                _googleMainPageObject.ValidateResults(expectedText);
                logger.Debug("True");
            }
            catch (AssertionException e)
            {
                _verificationErrors.Append("2 page: " + e.Message);
                logger.Debug("False");
            }

            logger.Info("Check page #3");
            _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("3")));
            _driver.FindElement(By.LinkText("3")).Click();
            try
            {
                _googleMainPageObject.ValidateResults(expectedText);
                logger.Debug("True");
            }
            catch (AssertionException e)
            {
                _verificationErrors.Append("3 page: " + e.Message);
                logger.Debug("False");
            }

            logger.Info("Check page #4");
            _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("4")));
            _driver.FindElement(By.LinkText("4")).Click();
            try
            {
                _googleMainPageObject.ValidateResults(expectedText);
                logger.Debug("True");
            }
            catch (AssertionException e)
            {
                _verificationErrors.Append("4 page: " + e.Message);
                logger.Debug("False");
            }

            logger.Info("Check page #5");
            _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("5")));
            _driver.FindElement(By.LinkText("5")).Click();
            try
            {
                _googleMainPageObject.ValidateResults(expectedText);
                logger.Debug("True");
            }
            catch (AssertionException e)
            {
                _verificationErrors.Append("5 page: " + e.Message);
                logger.Debug("False");
            }
        }
    }
}
