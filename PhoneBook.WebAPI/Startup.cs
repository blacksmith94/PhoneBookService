using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Data.Database;
using PhoneBook.WebAPI.Extensions;

namespace PhoneBook.WebAPI
{
	public class Startup
	{
		private IConfiguration _configuration;
		
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			//Infrastructure  
			services.AddInfrastructure(_configuration);

			//Data
			services.AddData(_configuration);

			//Domain
			services.AddDomain();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			if (env.IsEnvironment("Test"))
			{
				dbContext.Database.EnsureDeleted();
			}
			dbContext.Database.EnsureCreated();

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneBook API");
			});
		}
	}
}
