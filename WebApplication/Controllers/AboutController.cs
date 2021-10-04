using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
	public class AboutController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return RedirectToAction("Documents");
		}

		[HttpGet]
		public IActionResult Documents()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Plans()
		{
			return View();
		}

		[HttpGet]
		public IActionResult More()
		{
			return View();
		}
	}
}