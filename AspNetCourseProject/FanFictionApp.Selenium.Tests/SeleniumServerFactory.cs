namespace FanFictionApp.Selenium.Tests
{
	using System;
	using System.Linq;
	using System.Diagnostics;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.AspNetCore.Mvc.Testing;
	using Microsoft.AspNetCore.Hosting.Server.Features;

	public class SeleniumServerFactory<TStartup>
		: WebApplicationFactory<Startup> where TStartup : class
	{
		public string RootUri { get; set; } //Save this use by tests

		private readonly Process process;
		private IWebHost host;

		public SeleniumServerFactory()
		{
			ClientOptions.BaseAddress = new Uri("https://localhost"); //will follow redirects by default

			process = new Process()
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "selenium-standalone",
					Arguments = "start",
					UseShellExecute = true
				}
			};
			process.Start();
		}

		protected override TestServer CreateServer(IWebHostBuilder builder)
		{
			//Real TCP port
			host = builder.Build();
			host.Start();
			RootUri = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses
				.LastOrDefault(); //Last is https://localhost:5001!
								  //TODO:this should be with inMemoryDb but it just works for now not the correct or right way but
								  //Fake Server we won't use...this is lame. Should be cleaner, or a utility class

			return new TestServer(new WebHostBuilder().UseStartup<FakeStartUp>());
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				this.host.Dispose();
				this.process.CloseMainWindow(); //Be sure to stop Selenium Standalone
			}
		}
	}
}