using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Configs;
using PhoneBook.Domain.Model;

namespace PhoneBook.Data.Database
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{		
		}

		public DbSet<Person> Persons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new PersonConfig());
		}
	}
}
