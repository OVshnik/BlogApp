using BlogApp;
using BlogApp.Data;
using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Services;
using NLog;
using NLog.Fluent;
using NLog.Web;
using GenericUnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

		builder.Services.AddAutoMapper(typeof(MappingProfile)) ;

		builder.Services.AddDbContext<ApplicationDbContext>(options => 
			options.UseSqlServer(connection)).AddIdentity<User, Role>(opts =>
			{
				opts.Password.RequiredLength = 10;
				opts.Password.RequireNonAlphanumeric = false;
				opts.Password.RequireLowercase = false;
				opts.Password.RequireUppercase = false;
				opts.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders(); 


		builder.Services.AddTransient<IArticleRepository, ArticleRepository>()
			.AddTransient<ITagRepository, TagRepository>()
			.AddTransient<ICommentRepository, CommentRepository>();

		builder.Services.AddTransient<IUserService,UserService>();
		builder.Services.AddTransient<IArticleService, ArticleService>();
		builder.Services.AddTransient<ICommentService, CommentService>();
		builder.Services.AddTransient<ITagService, TagService>();
		builder.Services.AddTransient<IRoleService, RoleService>();

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	    .AddCookie(options =>
	    {
		options.LoginPath = "/Login";
		options.AccessDeniedPath = "/Login";
	    });
		builder.Services.AddAuthorizationBuilder()
			.AddPolicy("OnlyUberAdmin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "UberAdmin");
			})
			.AddPolicy("OnlyAdmin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "Admin", "UberAdmin");
			})
			.AddPolicy("OnlyModerator&Admin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "Moderator","Admin", "UberAdmin");
			})
			.AddPolicy("OnlyUser&Admin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "User", "Admin", "UberAdmin");
			});

		builder.Logging
			.ClearProviders()
			.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
			.AddConsole()
			.AddNLog("nlog");
		builder.Host.UseNLog();

		var app = builder.Build();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}