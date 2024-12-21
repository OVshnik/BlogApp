using BlogApp;
using BlogApp.Data;
using BlogApp.Data.Extensions;
using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Services;
using GenericUnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Claims;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		builder.Services.AddAutoMapper(typeof(MappingProfile));

		builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton)
			.AddUnitOfWork()
			.AddCustomRepository<Article, ArticleRepository>()
			.AddCustomRepository<Tag, TagRepository>()
			.AddCustomRepository<Comment, CommentRepository>();

		builder.Services.AddIdentity<User, Role>(opts =>
		{
			opts.Password.RequiredLength = 10;
			opts.Password.RequireNonAlphanumeric = false;
			opts.Password.RequireLowercase = false;
			opts.Password.RequireUppercase = false;
			opts.Password.RequireDigit = false;
		}).AddEntityFrameworkStores<ApplicationDbContext>();

		builder.Services.AddScoped(typeof(UserService));
		builder.Services.AddScoped(typeof(ArticleService));
		builder.Services.AddScoped(typeof(CommentService));
		builder.Services.AddScoped(typeof(TagService));

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	    .AddCookie(options =>
	    {
		options.LoginPath = "/Login";
		options.AccessDeniedPath = "/Login";
	    });
		builder.Services.AddAuthorizationBuilder()
			.AddPolicy("OnlyAdmin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "Admin");
			})
			.AddPolicy("OnlyModerator&Admin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "Moderator","Admin");
			})
			.AddPolicy("OnlyUser&Admin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "User", "Admin");
			});

		var app = builder.Build();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}