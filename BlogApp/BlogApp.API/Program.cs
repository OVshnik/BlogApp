using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlogApp.API;
using BlogApp.API.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(APIMappingProfile));

builder.Services.AddControllers().AddJsonOptions(options=>options.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

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

builder.Services.AddTransient<IUserService, UserService>();
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
			.AddPolicy("OnlyAdmin", policy =>
			{
				policy.RequireClaim(ClaimTypes.Role, "Admin", "UberAdmin");
			});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
