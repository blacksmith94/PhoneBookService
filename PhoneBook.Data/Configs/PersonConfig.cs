using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBook.Domain.Model;

namespace PhoneBook.Data.Configs
{
	public class PersonConfig : IEntityTypeConfiguration<Person>
	{
		public void Configure(EntityTypeBuilder<Person> builder)
		{
			builder.ToTable("person");
			
			builder.HasKey(f => f.Id);

			builder.Property(f => f.Id).HasColumnName("id").ValueGeneratedOnAdd();
			builder.Property(f => f.FirstName).HasColumnName("firstName");
			builder.Property(f => f.SecondName).HasColumnName("secondName");
			builder.Property(f => f.Address).HasColumnName("address");
			builder.Property(f => f.PhoneNumber).HasColumnName("phoneNumber");
		}
	}
}
