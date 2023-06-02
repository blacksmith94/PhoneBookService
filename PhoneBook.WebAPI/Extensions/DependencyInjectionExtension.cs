using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PhoneBook.Data;
using PhoneBook.Data.Database;
using PhoneBook.Data.Database.Repositories;
using PhoneBook.Domain.Abstractions;
using PhoneBook.Domain.Services;
using System;

namespace PhoneBook.WebAPI.Extensions
{
	public static class DependencyInjectionExtension
	{
		public static void AddDomain(this IServiceCollection services)
		{
			services.AddScoped<PhoneBookService>();
		}

		public static void AddData(this IServiceCollection services, IConfiguration configuration)
		{
			var sqlConfig = configuration.GetSection("Sql").Get<SqlOptions>();

			services.AddTransient<IPersonRepository, PersonRepository>();

			services.AddDbContext<DatabaseContext>(options =>
			{
				var dbSourceText = "Data Source=";
				var dbName = sqlConfig.DbConnectionString.Replace(dbSourceText, "", StringComparison.OrdinalIgnoreCase);
				var binPath = System.IO.Directory.GetParent(typeof(Program).Assembly.Location).FullName;
				var databaseLocation = System.IO.Path.Combine(binPath, dbName);
				options.UseSqlite($"{dbSourceText}{databaseLocation}");
			});
		}

		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook.WebAPI", Version = "v1" });
			});
			services.AddAutoMapper(typeof(Startup));
			services.AddControllers();
			services.AddHttpClient();
			services.AddMvc()
					.AddFluentValidation(mvcConfig => mvcConfig.RegisterValidatorsFromAssemblyContaining<Startup>());
		}
	}
}
