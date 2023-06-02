using PhoneBook.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Abstractions
{
	public interface IPersonRepository
    {
		Task<Person> CreateAsync(string firstName, string secondName, string address, string phoneNumber);

		Task<IEnumerable<Person>> FindAsync(string firstName, string secondName);

		Task Cleanup();
	}
}