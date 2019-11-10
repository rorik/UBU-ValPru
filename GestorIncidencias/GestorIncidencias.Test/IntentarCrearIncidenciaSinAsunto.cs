using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class IntentarCrearIncidenciaSinAsunto
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.katalon.com/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheIntentarCrearIncidenciaSinAsuntoTest()
        {
            driver.Navigate().GoToUrl("http://localhost:63119/");
            driver.FindElement(By.Id("centro")).Click();
            driver.FindElement(By.Id("centro")).Clear();
            driver.FindElement(By.Id("centro")).SendKeys("CentroA");
            driver.FindElement(By.Id("clave")).Clear();
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.Id("Login")).Click();
            driver.FindElement(By.Id("aula")).Click();
            new SelectElement(driver.FindElement(By.Id("aula"))).SelectByText("1-2");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Aula'])[1]/following::option[2]")).Click();
            driver.FindElement(By.Id("comentario")).Click();
            driver.FindElement(By.Id("comentario")).Clear();
            driver.FindElement(By.Id("comentario")).SendKeys("TestComentario");
            driver.FindElement(By.Id("equipo")).Clear();
            driver.FindElement(By.Id("equipo")).SendKeys("22A");
            driver.FindElement(By.Id("crearIncidencia")).Click();
            driver.FindElement(By.Id("error-message")).Click();
            Assert.AreEqual("Asunto no puede estar vacio.", driver.FindElement(By.Id("error-message")).Text);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
