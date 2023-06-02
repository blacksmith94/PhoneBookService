using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Abstractions;
using PhoneBook.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data.Database.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		private readonly DatabaseContext _context;

		public PersonRepository(DatabaseContext context)
		{
			_context = context;
		}
		
		public async Task<Person> CreateAsync(string firstName, string secondName, string address, string phoneNumber)
		{
			var person = (await _context.Persons.AddAsync(new Person()
			{
				FirstName = firstName,
				SecondName = secondName,
				PhoneNumber = phoneNumber,
				Address = address,
			})).Entity;

			await _context.SaveChangesAsync();
			return person;
		}

		public async Task<IEnumerable<Person>> FindAsync(string firstName, string secondName)
		{
			var query = _context.Persons.AsQueryable();

			if (firstName != null)
				query = query.Where(p => p.FirstName == firstName);

			if (secondName != null)
				query = query.Where(p => p.SecondName == secondName);

			var persons = await query.ToListAsync();

			return persons;
		}

		public async Task Cleanup() => await _context.Database.ExecuteSqlRawAsync("DELETE FROM Person");
	}
}
