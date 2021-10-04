using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
	public class ServicesController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return RedirectToAction("Groupage");
		}

		[HttpGet]
		public IActionResult Groupage()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Individual()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Delivery()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Storage()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Insurance()
		{
			return View();
		}
	}
}