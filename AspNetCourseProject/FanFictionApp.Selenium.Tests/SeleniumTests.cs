namespace FanFictionApp.Selenium.Tests
{
	using Xunit;
	using OpenQA.Selenium;
	using OpenQA.Selenium.Chrome;
	using OpenQA.Selenium.Remote;

	public class SeleniumTests : IClassFixture<SeleniumServerFactory<Startup>>
	{
		private readonly SeleniumServerFactory<Startup> server;
		private readonly IWebDriver browser;

		public SeleniumTests(SeleniumServerFactory<Startup> server)
		{
			this.server = server;
			server.CreateClient();
			var opts = new ChromeOptions();
			//opts.AddArgument("--headless"); //Optional, comment this out if you want to SEE the browser window
			opts.AddArgument("no-sandbox");
			this.browser = new RemoteWebDriver(opts);
		}

		[Fact]
		public void LoadTheMainPageAndCheckTitle()
		{
			browser.Navigate().GoToUrl(server.RootUri);
			Assert.StartsWith("Home Page", browser.Title);
		}
	}
}