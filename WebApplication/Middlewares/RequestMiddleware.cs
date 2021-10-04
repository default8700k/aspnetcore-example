using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WebApplication.Core;
using WebApplication.Models;

namespace WebApplication.Middlewares
{
	public class RequestMiddleware
	{
		private readonly RequestDelegate next;

		public RequestMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext, AppDbContext dbContext)
		{
			var request = new Request
			{
				Ip = httpContext.Connection.RemoteIpAddress,
				Url = httpContext.Request.Path,
				Method = httpContext.Request.Method,
				UserAgent = httpContext.Request.Headers["User-Agent"],
				DateTime = DateTime.Now
			};

			await dbContext.Requests.AddAsync(request);
			await dbContext.SaveChangesAsync();

			await next.Invoke(httpContext);
		}
	}
}