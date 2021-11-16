using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services.Abstractions;
using PeopleManager.Services.Model;

namespace PeopleManager.RestApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PeopleController : ControllerBase
	{
		private readonly IPersonService _personService;

		public PeopleController(IPersonService personService)
		{
			_personService = personService;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var person = _personService.Get(id);
			return Ok(person);
		}

		[HttpGet]
		public IActionResult Find()
		{
			var people = _personService.Find();
			return Ok(people);
		}

		[HttpPost]
		public IActionResult Create(PersonDto person)
		{
			var createdPerson = _personService.Create(person);
			return Ok(createdPerson);
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromRoute]int id, [FromBody]PersonDto person)
		{
			var updatedPerson = _personService.Update(id, person);
			return Ok(updatedPerson);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var isDeleted = _personService.Delete(id);
			if (!isDeleted)
			{
				return BadRequest();
			}

			return Ok();
		}
	}
}
