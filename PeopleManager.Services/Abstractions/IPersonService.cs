using System.Collections.Generic;
using PeopleManager.Model;
using PeopleManager.Services.Model;

namespace PeopleManager.Services.Abstractions
{
	public interface IPersonService
	{
		PersonDto Get(int id);
		IList<PersonDto> Find();

		PersonDto Create(PersonDto person);
		PersonDto Update(int id, PersonDto person);
		bool Delete(int id);
	}
}
