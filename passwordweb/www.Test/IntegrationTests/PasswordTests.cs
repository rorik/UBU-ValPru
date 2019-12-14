namespace www.Test.IntegrationTests
{
    public class PasswordTests
    {

        private const string BaseURL = "http://localhost:63119/";
        private IWebDriver Driver { get; set; }

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
            Assert.That(() => Driver.FindElement(By.Id("ctxInfo")), Throws.Nothing);
        }
    }
}
