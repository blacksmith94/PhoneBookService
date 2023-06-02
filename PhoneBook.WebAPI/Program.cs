using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PhoneBook.WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureLogging(options =>
					{
						options.AddConsole();
					});
					webBuilder.UseStartup<Startup>();
					webBuilder.UseUrls("http://0.0.0.0:7628/");
				});
	}
}
