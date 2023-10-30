using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Tabii.DataAccess.Data;
using Tabii.DataAccess.Repository;
using Tabii.DataAccess.Repository.IRepository;
using Tabii.Utilities;

namespace TabiiWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection")
				));
			builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
			builder.Services.AddIdentity<IdentityUser,IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			builder.Services.AddSingleton<IEmailSender, EmailSender>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Identity/Account/Login";
				options.LogoutPath = "/Identity/Account/Logout";
				options.AccessDeniedPath = "/Identity/Account/AccessDenined";
			});
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(100);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			string key = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
			StripeConfiguration.ApiKey = key;
			app.UseAuthentication(); ;

			app.UseAuthorization();
			app.UseSession();
			app.MapRazorPages();
			app.MapControllers();
			app.Run();
		}
	}
}