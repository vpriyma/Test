using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace EPAM_test
{
    [TestFixture]
    public class Tests: BaseTest
    {
        [Test]
        public void Test1()
        {
            Logger.Info("Start Test1");

            var request = "automation";

            //Search for “automation”.
            GoogleMainPageObj.Search(request);

            //Wait and click the first searched link
            GoogleMainPageObj.WaitAndClickElement(By.Id("vs0p1c0"));

            //Verify that title contains searched word
            Assert.IsTrue(GoogleMainPageObj.GetTitle().Contains(request), "Test1 is FAILED! Title doesn't contain \"{0}\"", request);

            Logger.Info("Test1 passed.");
        }

        [Test]
        public void Test2()
        {
            Logger.Info("Start Test2");

            var request = "automation";
            var expectedText = "testautomationday.com";

            //Search for “automation”.
            GoogleMainPageObj.Search(request);

            //Searching "testautomationday.com" in search results of 1, 2, 3, 4, 5 pages
            GoogleMainPageObj.VerifyTextResults(expectedText, 1);

            for (int i=2; i < 6; i++)
            {
                GoogleMainPageObj.WaitAndClickElement(By.LinkText(i.ToString()));
                GoogleMainPageObj.VerifyTextResults(expectedText, i);
            }

        }
    }
}
