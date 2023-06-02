using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PhoneBook.WebAPI;
using Xunit;

namespace PhoneBook.IntegrationTests
{
	[CollectionDefinition("Integration")]
	public sealed class IntegrationFixture : IDisposable, ICollectionFixture<IntegrationFixture>
	{
		private readonly IHost _host;
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public Request Request { get; }

		public IntegrationFixture()
		{
			Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
			var configBuilder = new ConfigurationBuilder()
						 .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true);
			var configRoot = configBuilder.Build();
			var hostBuilder = new HostBuilder()
						.ConfigureWebHost(webHost =>
						{
							webHost.UseTestServer();
							webHost.UseStartup<Startup>();
							webHost.ConfigureAppConfiguration(config =>
							{
								config.AddConfiguration(configRoot);
							});
						});


			_host = hostBuilder.Start();
			_server = _host.GetTestServer();
			_client = _server.CreateClient();

			Request = new Request(_client);
		}

		public void Dispose()
		{
			_client.Dispose();
			_server.Dispose();
		}
	}
}

