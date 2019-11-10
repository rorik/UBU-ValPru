using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System;

namespace GestorIncidencias.Test.IntegrationTests
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class IncidenciasTests<TWebDriver> : BaseTest where TWebDriver : IWebDriver, new()
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
        public void CreateNewIncidencia()
        {
            LoginTests.LoginAdmin();
            Click(Driver.FindElement(By.Id("nav-create")));
            Click(Driver.FindElement(By.Id("aula")));
            new SelectElement(Driver.FindElement(By.Id("aula"))).SelectByText("1-2");
            Driver.FindElement(By.Id("asunto")).Clear();
            Driver.FindElement(By.Id("asunto")).SendKeys("TestAsunto");
            Driver.FindElement(By.Id("comentario")).Clear();
            Driver.FindElement(By.Id("comentario")).SendKeys("TestComentario");
            Driver.FindElement(By.Id("equipo")).Clear();
            Driver.FindElement(By.Id("equipo")).SendKeys("22A");
            Click(Driver.FindElement(By.Id("crearIncidencia")));
            Assert.That(Driver.FindElement(By.Id("ok-message")).Text, Is.EqualTo("La incidendia ha sido enviada correctamente."));
            Click(Driver.FindElement(By.Id("nav-incidencias")));
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.Nothing);
            var lastIncidencia = Driver.FindElements(By.Name("asunto")).Select(i => int.Parse(i.GetAttribute("id").Split("-").Last())).Max();
            Assert.That(Driver.FindElement(By.Id($"asunto-{lastIncidencia}")).Text, Is.EqualTo("TestAsunto"));
            Assert.That(Driver.FindElement(By.Id($"comentario-{lastIncidencia}")).Text, Is.EqualTo("TestComentario"));
            Assert.That(Driver.FindElement(By.Id($"centroId-{lastIncidencia}")).Text, Is.EqualTo("CentroA"));
            Assert.That(Driver.FindElement(By.Id($"aula-{lastIncidencia}")).Text, Is.EqualTo("1-2"));
            Assert.That(Driver.FindElement(By.Id($"equipo-{lastIncidencia}")).Text, Is.EqualTo("22A"));
        }

        [Test]
        public void CreateIncidenciaWithoutAsunto()
        {
            LoginTests.LoginAdmin();
            Click(Driver.FindElement(By.Id("nav-create")));
            Click(Driver.FindElement(By.Id("aula")));
            new SelectElement(Driver.FindElement(By.Id("aula"))).SelectByText("1-1");
            Driver.FindElement(By.Id("comentario")).Clear();
            Driver.FindElement(By.Id("comentario")).SendKeys("TestError");
            Driver.FindElement(By.Id("equipo")).Clear();
            Driver.FindElement(By.Id("equipo")).SendKeys("ABCD");
            Click(Driver.FindElement(By.Id("crearIncidencia")));
            Assert.That(Driver.FindElement(By.Id("error-message")).Text, Is.EqualTo("Asunto no puede estar vacio."));
            Click(Driver.FindElement(By.Id("nav-incidencias")));
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.Nothing);
            var incidencia = Driver.FindElements(By.Name("asunto")).FirstOrDefault(i => i.Text == "TestError");
            Assert.That(incidencia, Is.Null);
        }

        [Test]
        public void CloseIncidencia()
        {
            var id = new Random().Next(1000);
            LoginTests.LoginAdmin();
            Click(Driver.FindElement(By.Id("nav-create")));
            Click(Driver.FindElement(By.Id("aula")));
            new SelectElement(Driver.FindElement(By.Id("aula"))).SelectByText("2-1");
            Driver.FindElement(By.Id("asunto")).Clear();
            Driver.FindElement(By.Id("asunto")).SendKeys($"DeleteThis{id}");
            Driver.FindElement(By.Id("comentario")).Clear();
            Driver.FindElement(By.Id("comentario")).SendKeys("Comment");
            Driver.FindElement(By.Id("equipo")).Clear();
            Driver.FindElement(By.Id("equipo")).SendKeys("<>");
            Click(Driver.FindElement(By.Id("crearIncidencia")));
            Assert.That(Driver.FindElement(By.Id("ok-message")).Text, Is.EqualTo("La incidendia ha sido enviada correctamente."));
            Click(Driver.FindElement(By.Id("nav-incidencias")));
            Assert.That(() => Driver.FindElement(By.Id("incidencias-page")), Throws.Nothing);
            var index = Driver.FindElements(By.Name("asunto")).First(i => i.Text == $"DeleteThis{id}").GetAttribute("id").Split("-").Last();
            Assert.That(() => Driver.FindElement(By.Id($"cerrar-{index}")), Throws.Nothing);
            Click(Driver.FindElement(By.Id($"cerrar-{index}")));
            Assert.That(() => Driver.FindElement(By.Id($"cerrar-{index}")), Throws.InstanceOf(typeof(NoSuchElementException)));
        }


    }
}
