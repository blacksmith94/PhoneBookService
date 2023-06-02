using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Domain.Model;
using PhoneBook.Domain.Services;
using PhoneBook.WebAPI.Definitions;
using System.Net;
using System.Threading.Tasks;

namespace PhoneBook.WebAPI.Controllers
{
    [ApiController]
    [Route("person")]
    public class PersonController : ControllerBase
    {
        private readonly PhoneBookService _phoneBookService;
        private readonly IMapper _mapper;

		public PersonController(PhoneBookService phoneBookService, IMapper mapper)
		{
			_phoneBookService = phoneBookService;
			_mapper = mapper;
		}

        [HttpPost()]
		[ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<Person>> AddPerson(PersonDefinition definition)
        {
            var newPerson = _mapper.Map<Person>(definition);
            
            var person = await _phoneBookService.AddPerson(newPerson);

            return Ok(person);
        }

        [HttpGet()]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<IActionResult> FindPerson(string firstName, string lastName)
        {
           var persons = await _phoneBookService.Find(firstName, lastName);

            return Ok(persons);
        }

		[HttpDelete()]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<IActionResult> Cleanup()
		{
			await _phoneBookService.Cleanup();

			return Ok();
		}
	}
}
