using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Sdk;
using PeopleManager.Services.Model;

namespace PeopleManager.Ui.Mvc.Controllers
{
	public class PeopleController : Controller
	{
		private readonly PeopleApi _peopleApi;

		public PeopleController(PeopleApi peopleApi)
		{
			_peopleApi = peopleApi;
		}

		public async Task<IActionResult> Index()
		{
			var people = await _peopleApi.Find();
			return View(people);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(PersonDto person)
		{
			if (!string.IsNullOrEmpty(person.FirstName) && person.FirstName.ToLower().Trim().Contains("john"))
			{
				ModelState.AddModelError("", "People with the name John are not welcome here!");
			}

			if (!ModelState.IsValid)
			{
				return View(person);
			}

			await _peopleApi.Create(person);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var person = await _peopleApi.Get(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(PersonDto person)
		{
			if (!ModelState.IsValid)
			{
				return View(person);
			}

			await _peopleApi.Update(person.Id, person);
			
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var person = await _peopleApi.Get(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		[Route("People/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _peopleApi.Delete(id);

			return RedirectToAction("Index");
		}
	}
}
