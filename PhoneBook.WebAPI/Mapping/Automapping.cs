using AutoMapper;
using PhoneBook.Domain.Model;
using PhoneBook.WebAPI.Definitions;

namespace PhoneBook.WebAPI.Mapping
{
    public class Automapping : Profile
    {
        public Automapping()
        {
			CreateMap<PersonDefinition, Person>();
		}
	}
}
