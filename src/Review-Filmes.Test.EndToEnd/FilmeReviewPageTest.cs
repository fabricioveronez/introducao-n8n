using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;

namespace Review_Filmes.Test.EndToEnd
{
	public class FilmeReviewPageTest
	{
		private IWebDriver driver;
		private WebDriverWait wait;
		private string url;

		[SetUp]
		public void Setup()
		{
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless", "no-sandbox", "disable-dev-shm-usage");
            driver = new ChromeDriver(chromeOptions);

			//driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

			var configuration = new ConfigurationBuilder()
					.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
					.AddJsonFile("appsettings.json", optional: true)
					.AddEnvironmentVariables()
					.Build();

			url = (configuration.GetSection("BASE_URL").Value != null) ? configuration.GetSection("BASE_URL").Value : "http://localhost:8080";
		}

		[Test]
		public void TesteCadastroReview()
		{
			driver.Navigate().GoToUrl(url + "/filme/1");
			var nome = driver.FindElement(By.Id("Nome"));
			nome.SendKeys("Teste");

			var avaliacao = driver.FindElement(By.Id("Avaliacao"));
			avaliacao.SendKeys("Foi bom");

			var submit = driver.FindElement(By.Id("submit"));
			submit.Click();

			wait.Until(e => e.FindElement(By.Id("comentarios")));

			var comentarios = driver.FindElement(By.Id("comentarios"));

			var nomeAvaliacao = comentarios.FindElement(By.TagName("h2")).Text.Contains("Teste");

			var descricaoAvaliacao = comentarios.FindElement(By.TagName("p")).Text.Contains("Foi bom");

			Assert.That(nomeAvaliacao, Is.EqualTo(true));
			Assert.That(descricaoAvaliacao, Is.EqualTo(true));
		}

		[TearDown]
		public void TearDown()
		{
			driver.Close();
		}
	}
}
