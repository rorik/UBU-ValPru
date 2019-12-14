namespace www.Test.IntegrationTests
{
    public class PasswordTests
    {

        private IWebDriver Driver { get; set; }
        private const string BaseURL = "http://localhost:63119/";
        private const string Cuenta = "TestCuenta";
        private const string Password = "testclave1234";
        private const string Password2 = "AbCdEfG1234567890";
        private const string Password3 = "ABCDEFG1234567890";
        private const string PasswordChangedMessage = "cambiada";
        private const string DuplicatedMismatchError = "coinciden";

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver()
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
            Driver.FindElement(By.Id("change-pwd-btn")).Click();
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
            Assert.That(() => Driver.FindElement(By.Xpath($"//*[contains(., '{PasswordChangedMessage}')]")), Throws.Nothing);
        }

        [Test]
        public void MismatchConfirmationPassword()
        {
            Driver.FindElement(By.Id("change-pwd-btn")).Click();
            Driver.FindElement(By.Id("ctxUser")).SendKeys(Cuenta);
            Driver.FindElement(By.Id("ctxPassword")).SendKeys(Password);
            Driver.FindElement(By.Id("ctxNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxReNewPassword")).SendKeys(Password3);
            Driver.FindElement(By.Id("ctxSubmit")).Click();
            Assert.That(() => Driver.FindElement(By.Xpath($"//*[contains(., '{DuplicatedMismatchError}')]")), Throws.Nothing);
        }

        [Test]
        public void ValidConfirmationPassword()
        {
            Driver.FindElement(By.Id("change-pwd-btn")).Click();
            Driver.FindElement(By.Id("ctxUser")).SendKeys(Cuenta);
            Driver.FindElement(By.Id("ctxPassword")).SendKeys(Password);
            Driver.FindElement(By.Id("ctxNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxReNewPassword")).SendKeys(Password2);
            Driver.FindElement(By.Id("ctxSubmit")).Click();
            Assert.That(() => Driver.FindElement(By.Xpath($"//*[contains(., '{DuplicatedMismatchError}')]")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }
    }
}
