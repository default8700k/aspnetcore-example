using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebApplication.Core;
using WebApplication.Hubs;
using WebApplication.Middlewares;

namespace WebApplication
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
			services.Configure<WebEncoderOptions>(options => { options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All); });

			services.AddSignalR();
			services.AddControllersWithViews()
				.AddViewOptions(options => { options.HtmlHelperOptions.ClientValidationEnabled = false; });

			services.AddAntiforgery(options =>
			{
				options.FormFieldName = "antiforgery";
				options.HeaderName = "X-CSRF-TOKEN-ASPNETCORE-EXAMPLE";
				options.SuppressXFrameOptionsHeader = false;
			});

			services.AddDbContext<AppDbContext>(options =>
			{
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment() == true) { app.UseDeveloperExceptionPage(); }
			else
			{
				app.UseExceptionHandler("/error");
				app.UseHsts();
			}

			app.UseRouting();
			app.UseStaticFiles();

			app.UseMiddleware<RequestMiddleware>();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<AdminHub>("/hubs/admin");

				#region [ Controller: Services ]
				endpoints.MapControllerRoute(
					name: "services",
					pattern: "services/{action}",
					defaults: new { controller = "Services", action = "Index" }
				);
				#endregion

				#region [ Controller: About ]
				endpoints.MapControllerRoute(
					name: "about",
					pattern: "about/{action}",
					defaults: new { controller = "About", action = "Index" }
				);
				#endregion

				#region [ Controller: Home ]
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{action}",
					defaults: new { controller = "Home", action = "Index" }
				);
				#endregion
			});
		}
	}
}