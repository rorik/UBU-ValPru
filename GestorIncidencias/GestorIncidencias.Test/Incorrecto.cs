using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GestorIncidencias.Test
{
    [TestFixture]
    public class Incorrecto
    {
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
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
        }

        [Test]
        public void TheIncorrectoTest()
        {
            driver.Navigate().GoToUrl("http://localhost:63119/");
            driver.FindElement(By.Id("centro")).Clear();
            driver.FindElement(By.Id("centro")).SendKeys("CentroA");
            driver.FindElement(By.Id("clave")).Clear();
            driver.FindElement(By.Id("clave")).SendKeys("1235");
            driver.FindElement(By.Id("Login")).Click();
            Assert.AreEqual("Contraseña incorrecta.", driver.FindElement(By.Id("Error")).Text);
        }
      
    }
}
