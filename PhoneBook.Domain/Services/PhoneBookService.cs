using PhoneBook.Domain.Abstractions;
using PhoneBook.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Services
{
	public class PhoneBookService
	{
		private readonly IPersonRepository _personRepository;

		public PhoneBookService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}

		public Task<Person> AddPerson(Person newPerson)
		{
			return _personRepository.CreateAsync(
				newPerson.FirstName,
				newPerson.SecondName,
				newPerson.Address,
				newPerson.PhoneNumber
			);
		}

		public async Task<IEnumerable<Person>> Find(string firstName, string lastName) => await _personRepository.FindAsync(firstName, lastName);

		public async Task Cleanup() => await _personRepository.Cleanup();
	}
}
