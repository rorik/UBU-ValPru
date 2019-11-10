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
    public class NavBarTests<TWebDriver> : BaseTest where TWebDriver : IWebDriver, new()
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
        public void NavBarLogin()
        {
            Assert.That(() => Driver.FindElement(By.Id("nav-login")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-create")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("nav-incidencias")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }

        [Test]
        public void NavBarUser()
        {
            LoginTests.LoginUser();
            Assert.That(() => Driver.FindElement(By.Id("nav-create")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-incidencias")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("nav-login")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }

        [Test]
        public void NavBarAdmin()
        {
            LoginTests.LoginAdmin();
            Assert.That(() => Driver.FindElement(By.Id("nav-create")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-incidencias")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-login")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }

        [Test]
        public void NavBarLogout()
        {
            LoginTests.LogoutUser();
            Assert.That(() => Driver.FindElement(By.Id("nav-login")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-incidencias")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("nav-create")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("logout-button")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }

        [Test]
        public void NavBarSwitchAdminToUser()
        {
            LoginTests.SwitchAdminToUser();
            Assert.That(() => Driver.FindElement(By.Id("nav-create")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-incidencias")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("nav-login")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }

        [Test]
        public void NavBarSwitchUserToAdmin()
        {
            LoginTests.SwitchUserToAdmin();
            Assert.That(() => Driver.FindElement(By.Id("nav-create")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-incidencias")), Throws.Nothing);
            Assert.That(() => Driver.FindElement(By.Id("nav-login")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }


    }
}
