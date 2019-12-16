using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace www.Test.IntegrationTests
{
    public class PasswordTests
    {

        private IWebDriver Driver { get; set; }
        private const string BaseURL = "http://localhost:63119/";
        private const string Cuenta = "TestCuenta";
        private const string Password = "testClave1234";
        private const string Password2 = "AbCdEfG1234567890";
        private const string Password3 = "ABCDEFG1234567890";
        private const string PasswordChangedMessage = "cambiada";
        private const string DuplicatedMismatchError = "coinciden";

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            {
                Url = BaseURL
            };
        }

        [TearDown]
        protected void TearDown()
        {
            try
            {
                Driver.Quit();
            }
            catch { }
        }

        [Test]
        public void AccessWeb()
        {
            Assert.That(() => Driver.FindElement(By.Id("change-pwd-btn")), Throws.Nothing);
            Driver.FindElement(By.Id("change-pwd-btn")).Click();
            Assert.That(() => Driver.FindElement(By.Id("password-change-page")), Throws.Nothing);
        }

        [Test]
        public void ChangePassword()
        {
            AccessWeb();
            Assert.That(() => Driver.FindElement(By.Id("ctxUser")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("ctxPassword")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("ctxNewPassword")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("ctxReNewPassword")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("ctxSubmit")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("ctxInfo")), Throws.Nothing);
            Driver.FindElement(By.Id("ctxUser")).SendKeys(Cuenta);
            Driver.FindElement(By.Id("ctxPassword")).SendKeys(Password);
            Driver.FindElement(By.Id("ctxNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxReNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxSubmit")).Click();
            Assert.That(() => Driver.FindElement(By.XPath($"//*[contains(., '{PasswordChangedMessage}')]")), Throws.Nothing);
        }

        [Test]
        public void MismatchConfirmationPassword()
        {
            AccessWeb();
            Driver.FindElement(By.Id("ctxUser")).SendKeys(Cuenta);
            Driver.FindElement(By.Id("ctxPassword")).SendKeys(Password);
            Driver.FindElement(By.Id("ctxNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxReNewPassword")).SendKeys(Password3);
            Driver.FindElement(By.Id("ctxSubmit")).Click();
            Assert.That(() => Driver.FindElement(By.XPath($"//*[contains(., '{DuplicatedMismatchError}')]")), Throws.Nothing);
        }

        [Test]
        public void ValidConfirmationPassword()
        {
            AccessWeb();
            Driver.FindElement(By.Id("ctxUser")).SendKeys(Cuenta);
            Driver.FindElement(By.Id("ctxPassword")).SendKeys(Password);
            Driver.FindElement(By.Id("ctxNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxReNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxSubmit")).Click();
            Assert.That(() => Driver.FindElement(By.XPath($"//*[contains(., '{DuplicatedMismatchError}')]")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }

        [Test]
        public void NavigateByTab()
        {
            AccessWeb();
            Driver.FindElement(By.Id("ctxUser")).Click();
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.EqualTo(Driver.FindElement(By.Id("ctxUser"))));
            Driver.FindElement(By.Id("ctxUser")).SendKeys(Keys.Tab);
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.Not.EqualTo(Driver.FindElement(By.Id("ctxUser"))));
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.EqualTo(Driver.FindElement(By.Id("ctxPassword"))));
            Driver.FindElement(By.Id("ctxPassword")).SendKeys(Keys.Tab);
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.Not.EqualTo(Driver.FindElement(By.Id("ctxPassword"))));
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.EqualTo(Driver.FindElement(By.Id("ctxNewPassword"))));
            Driver.FindElement(By.Id("ctxNewPassword")).SendKeys(Keys.Tab);
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.Not.EqualTo(Driver.FindElement(By.Id("ctxNewPassword"))));
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.EqualTo(Driver.FindElement(By.Id("ctxReNewPassword"))));
            Driver.FindElement(By.Id("ctxReNewPassword")).SendKeys(Keys.Tab);
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.Not.EqualTo(Driver.FindElement(By.Id("ctxReNewPassword"))));
            Assert.That(Driver.SwitchTo().ActiveElement(), Is.EqualTo(Driver.FindElement(By.Id("ctxSubmit"))));
        }
    }
}
