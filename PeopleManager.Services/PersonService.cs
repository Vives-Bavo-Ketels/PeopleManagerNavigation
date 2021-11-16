using System;
using System.Collections.Generic;
using System.Linq;
using PeopleManager.Data;
using PeopleManager.Model;
using PeopleManager.Services.Abstractions;
using PeopleManager.Services.Model;

namespace PeopleManager.Services
{
	public class PersonService : IPersonService
	{
		private readonly PeopleManagerDbContext _peopleManagerDbContext;

		public PersonService(PeopleManagerDbContext peopleManagerDbContext)
		{
			_peopleManagerDbContext = peopleManagerDbContext;
		}

		public PersonDto Get(int id)
		{
			var person = _peopleManagerDbContext.People
				.Select(p => new PersonDto
				{
					Id = p.Id,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Age = p.Age,
					Email = p.Email
				})
				.SingleOrDefault(p => p.Id == id);

			return person;
		}

		public IList<PersonDto> Find()
		{
			var people = _peopleManagerDbContext.People
				.Select(p => new PersonDto
				{
					Id = p.Id,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Age = p.Age,
					Email = p.Email
				})
				.ToList();

			return people;
		}


		public PersonDto Create(PersonDto dto)
		{
			var person = new Person
			{
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Age = dto.Age,
				Email = dto.Email,
				CreatedAt = DateTime.UtcNow
			};

			_peopleManagerDbContext.People.Add(person);
			_peopleManagerDbContext.SaveChanges();

			return Get(person.Id);
		}

		public PersonDto Update(int id, PersonDto person)
		{
			var dbPerson = _peopleManagerDbContext.People
				.SingleOrDefault(p => p.Id == id);

			if (dbPerson is null)
			{
				return null;
			}

			dbPerson.FirstName = person.FirstName;
			dbPerson.LastName = person.LastName;
			dbPerson.Email = person.Email;
			dbPerson.Age = person.Age;

			_peopleManagerDbContext.SaveChanges();

			return Get(dbPerson.Id);
		}

		public bool Delete(int id)
		{
			var dbPerson = _peopleManagerDbContext.People
				.SingleOrDefault(p => p.Id == id);

			if (dbPerson is null)
			{
				return false;
			}

			_peopleManagerDbContext.People.Remove(dbPerson);
			_peopleManagerDbContext.SaveChanges();

			return true;
		}
	}
}
