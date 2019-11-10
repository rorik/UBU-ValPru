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
    public class LoginTests<TWebDriver> : BaseTest where TWebDriver : IWebDriver, new()
    {

        [SetUp]
        public void SetupTest()
        {
            SetUp(new TWebDriver());
        }

        [TearDown]
        public void TeardownTest()
        {
            TearDown();
        }

        [Test]
        public void LoginUser()
        {
            Driver.FindElement(By.Id("centro")).Clear();
            Driver.FindElement(By.Id("centro")).SendKeys("CentroA");
            Driver.FindElement(By.Id("clave")).Clear();
            Driver.FindElement(By.Id("clave")).SendKeys("1234");
            Click(Driver.FindElement(By.Id("Login")));
            Assert.That(() => Driver.FindElement(By.Id("create-page")), Throws.Nothing);
            Assert.That(Driver.Title, Is.EqualTo("Crear Incidencia"));
        }

        [Test]
        public void LoginAdmin()
        {
            Driver.FindElement(By.Id("centro")).Clear();
            Driver.FindElement(By.Id("centro")).SendKeys("CentroA");
            Driver.FindElement(By.Id("clave")).Clear();
            Driver.FindElement(By.Id("clave")).SendKeys("admin1234");
            Click(Driver.FindElement(By.Id("Login")));
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.Nothing);
            Assert.That(Driver.Title, Is.EqualTo("Incidencias"));
        }

        [Test]
        public void LogoutUser()
        {
            LoginUser();
            Assert.That(() => Driver.FindElement(By.Id("logout-button")), Throws.Nothing);
            Click(Driver.FindElement(By.Id("logout-button")));
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.Nothing);
            Assert.That(Driver.Title, Is.EqualTo("Login"));
        }

        [Test]
        public void LogoutAdmin()
        {
            LoginUser();
            Assert.That(() => Driver.FindElement(By.Id("logout-button")), Throws.Nothing);
            Click(Driver.FindElement(By.Id("logout-button")));
            Assert.That(() => Driver.FindElement(By.Id("login-page")), Throws.Nothing);
            Assert.That(Driver.Title, Is.EqualTo("Login"));
        }

        [Test]
        public void SwitchAdminToUser()
        {
            LogoutAdmin();
            LoginUser();
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("create-page")), Throws.Nothing);
        }

        [Test]
        public void SwitchUserToAdmin()
        {
            LogoutUser();
            LoginAdmin();
            Assert.That(() => Driver.FindElement(By.Id("create-page")), Throws.InstanceOf(typeof(NoSuchElementException)));
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.Nothing);
        }

        [Test]
        public void LoginWrongPassword()
        {
            Driver.FindElement(By.Id("centro")).Clear();
            Driver.FindElement(By.Id("centro")).SendKeys("CentroA");
            Driver.FindElement(By.Id("clave")).Clear();
            Driver.FindElement(By.Id("clave")).SendKeys("1235");
            Click(Driver.FindElement(By.Id("Login")));
            Assert.That(Driver.FindElement(By.Id("Error")).Text, Is.EqualTo("Contraseña incorrecta."));
        }

        [Test]
        public void LoginWrongCentroCaseSensitive()
        {
            Driver.FindElement(By.Id("centro")).Clear();
            Driver.FindElement(By.Id("centro")).SendKeys("Centroa");
            Driver.FindElement(By.Id("clave")).Clear();
            Driver.FindElement(By.Id("clave")).SendKeys("1234");
            Click(Driver.FindElement(By.Id("Login")));
            Assert.That(Driver.FindElement(By.Id("Error")).Text, Is.EqualTo("Centro inválido."));
        }

        [Test]
        public void LoginWrongCentro()
        {
            Driver.FindElement(By.Id("centro")).Clear();
            Driver.FindElement(By.Id("centro")).SendKeys("?");
            Driver.FindElement(By.Id("clave")).Clear();
            Driver.FindElement(By.Id("clave")).SendKeys("1234");
            Click(Driver.FindElement(By.Id("Login")));
            Assert.That(Driver.FindElement(By.Id("Error")).Text, Is.EqualTo("Centro inválido."));
        }

    }
}
