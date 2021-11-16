using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Sdk;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly PeopleApi _peopleApi;

		public HomeController(PeopleApi peopleApi)
		{
			_peopleApi = peopleApi;
		}

		public async Task<IActionResult> Index()
		{
			var people = await _peopleApi.Find();
			return View(people);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		
	}
}
