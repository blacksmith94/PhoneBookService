using PhoneBook.Domain.Model;
using PhoneBook.WebAPI.Definitions;
using Xunit;

namespace PhoneBook.IntegrationTests
{
	[Collection("Integration")]

	public class PhoneBookTests
	{
		private readonly Request _request;

		public PhoneBookTests(IntegrationFixture fixture)
		{
			_request = fixture.Request;
		}

		[Theory]
		[InlineData("John", "Smith", "1234 Sand Hill Dr, Royal Oak, MI", "(248) 123 - 4567")]
		[InlineData("Cynthia", "Smith", "875 Main St, Ann Arbor, MI", "(824) 128-8758")]
		public async Task CanCreatePersons(string firstName, string secondName, string address, string phoneNumber)
		{
			var person = await AddPerson(firstName, secondName, address, phoneNumber);

			Assert.Equal(firstName, person.FirstName);
			Assert.Equal(secondName, person.SecondName);
			Assert.Equal(address, person.Address);
			Assert.Equal(phoneNumber, person.PhoneNumber);
		}

		[Fact]
		public async Task CanFindPersons()
		{
			var commonSecondName = "Smithers";
			var person1 = await AddPerson("Person1FirstName", commonSecondName, "1234 Sand Hill Dr, Royal Oak, MI", "(248) 123-4567");
			var person2 = await AddPerson("Person2FirstName", commonSecondName, "875 Main St, Ann Arbor, MI", "(824) 128-8758");

			var person1Result = (await GetPersons(person1.FirstName, person1.SecondName)).Single();
			var person2Result = (await GetPersons(person2.FirstName, person2.SecondName)).Single();

			var addedPersons = await GetPersons(null, commonSecondName);

			Assert.Equal(2, addedPersons.Count());

			AssertEqualPerson(person1, person1Result);
			AssertEqualPerson(person2, person2Result);
		}

		[Fact]
		public async Task CanCleanupPersons()
		{
			var person1 = await AddPerson("John", "Smith", "1234 Sand Hill Dr, Royal Oak, MI", "(248) 123 - 4567");
			var person2 = await AddPerson("Cynthia", "Smith", "875 Main St, Ann Arbor, MI", "(824) 128-8758");

			await _request.Delete("person");

			var person1Result = await GetPersons(person1.FirstName, person1.SecondName);
			var person2Result = await GetPersons(person2.FirstName, person2.SecondName);
			var personsResult = await GetPersons(null, null);

			Assert.Empty(person1Result);
			Assert.Empty(person2Result);
			Assert.Empty(personsResult);
		}

		private async Task<Person> AddPerson(string firstName, string secondName, string address, string phoneNumber)
		{
			var definition = new PersonDefinition()
			{
				FirstName = firstName,
				SecondName = secondName,
				Address = address,
				PhoneNumber = phoneNumber
			};
			var person = await _request.Post<Person>($"person", definition);
			return person;
		}

		private async Task<IEnumerable<Person>> GetPersons(string firstName, string secondName)
		{
			return await _request.Get<IEnumerable<Person>>($"person?firstName={firstName}&secondName={secondName}");
		}

		private void AssertEqualPerson(Person expected, Person actual)
		{
			Assert.Equal(expected.FirstName, actual.FirstName);
			Assert.Equal(expected.SecondName, actual.SecondName);
			Assert.Equal(expected.Address, actual.Address);
			Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
		}
	}
}
