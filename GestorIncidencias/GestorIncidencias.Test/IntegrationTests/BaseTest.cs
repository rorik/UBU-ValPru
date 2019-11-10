using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;

namespace GestorIncidencias.Test.IntegrationTests
{
    public class BaseTest
    {
        public const string BaseURL = "http://localhost:63119/";

        protected static IWebDriver Driver { get; private set; }
        private static bool IsIE { get; set; }

        protected void SetUp(IWebDriver driver)
        {
            driver.Url = BaseURL;
            IsIE = driver.GetType() == typeof(InternetExplorerDriver);
            if (IsIE)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            }
            Driver = driver;
        }
        protected void TearDown()
        {
            try
            {
                Driver.Quit();
            }
            catch { }
        }

        protected void Click(IWebElement element)
        {
            if (IsIE)
            {
                element.SendKeys(Keys.Enter);
            }
            else
            {
                element.Click();
            }
        }
    }
}
