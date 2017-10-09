using System.Text;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium.IE;

namespace EPAM_test
{
    public class BaseTest
    {
        public static ILog Logger { get; } =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainPageObject GoogleMainPageObj;
        public static StringBuilder VerificationErrors;

        [SetUp]
        public void SetupTest()
        {
            GoogleMainPageObj = new MainPageObject(new InternetExplorerDriver());
            VerificationErrors = new StringBuilder();

            // Open IE and Navigate to google.com.ua
            GoogleMainPageObj.Navigate();
        }

        [TearDown]
        public void TeardownTest()
        {
            GoogleMainPageObj.Quit();
            Assert.AreEqual("", VerificationErrors.ToString());
        }
    }
}