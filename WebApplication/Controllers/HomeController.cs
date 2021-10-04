using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication.Core;
using WebApplication.Hubs;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger logger;
		private readonly AppDbContext dbContext;
		private readonly IHubContext<AdminHub> hubContext;

		public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, IHubContext<AdminHub> hubContext)
		{
			this.logger = logger;
			this.dbContext = dbContext;
			this.hubContext = hubContext;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(Call call)
		{
			var validation = call.Validate();
			if (validation.IsValid == false)
			{
				var errors = validation.Errors;
				return StatusCode(400, errors);
			}

			call.Ip = HttpContext.Connection.RemoteIpAddress;
			call.DateTime = DateTime.Now;

			try
			{
				await dbContext.Calls.AddAsync(call);
				await dbContext.SaveChangesAsync();
			}
			catch (Exception exception)
			{
				logger.Log(LogLevel.Error, "{0}", exception.Message);
				return StatusCode(500);
			}

			await hubContext.Clients.Group("Authorized").SendAsync("Call", "Insert", call.Id);
			return StatusCode(200);
		}

		[HttpGet]
		public IActionResult Calculator()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Conditions()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Order()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Order(Order order)
		{
			var validation = order.Validate();
			if (validation.IsValid == false)
			{
				var errors = validation.Errors;
				return StatusCode(400, errors);
			}

			order.Ip = HttpContext.Connection.RemoteIpAddress;
			order.DateTime = DateTime.Now;

			try
			{
				await dbContext.Orders.AddAsync(order);
				await dbContext.SaveChangesAsync();
			}
			catch (Exception exception)
			{
				logger.Log(LogLevel.Error, "{0}", exception.Message);
				return StatusCode(500);
			}

			await hubContext.Clients.Group("Authorized").SendAsync("Order", "Insert", order.Id);
			return StatusCode(200);
		}

		[HttpGet]
		public IActionResult Terms()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Error()
		{
			return View();
		}
	}
}