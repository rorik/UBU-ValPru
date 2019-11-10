using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace GestorIncidencias.Test.IntegrationTests
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class RedirectTests<TWebDriver> : BaseTest where TWebDriver : IWebDriver, new()
    {
        public LoginTests<TWebDriver> LoginTests { get; set; }

        [SetUp]
        public void SetupTest()
        {
            SetUp(new TWebDriver());
            LoginTests = new LoginTests<TWebDriver>();
        }

        [TearDown]
        public void TeardownTest()
        {
            TearDown();
        }

        [Test]
        public void IndexRedirectUser()
        {
            LoginTests.LoginUser();
            Driver.Navigate().GoToUrl($"{BaseURL}/Home/Index");
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("create-page")), Throws.Nothing);
            Driver.Navigate().GoToUrl(BaseURL);
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("create-page")), Throws.Nothing);
        }

        [Test]
        public void IndexRedirectAdmin()
        {
            LoginTests.LoginAdmin();
            Driver.Navigate().GoToUrl($"{BaseURL}/Home/Index");
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.Nothing);
            Driver.Navigate().GoToUrl(BaseURL);
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.Nothing);
        }

        [Test]
        public void IncidenciasRedirectNotLoggedIn()
        {
            Driver.Navigate().GoToUrl($"{BaseURL}/Home/Incidencias");
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.Nothing);
            LoginTests.LogoutAdmin();
            Driver.Navigate().GoToUrl($"{BaseURL}/Home/Incidencias");
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.Nothing);
        }

        [Test]
        public void IncidenciasRedirectUser()
        {
            LoginTests.LoginUser();
            Driver.Navigate().GoToUrl($"{BaseURL}/Home/Incidencias");
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("create-page")), Throws.Nothing);
        }

        [Test]
        public void CreateRedirectNotLoggedIn()
        {
            Driver.Navigate().GoToUrl($"{BaseURL}/Home/Create");
            Assert.That(() => Driver.FindElement(By.Id("create-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.Nothing);
        }


    }
}
