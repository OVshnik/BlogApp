using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
	public class ApplicationDbContext:IdentityDbContext<User,Role,string>
	{
		public DbSet<Article> Articles { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			if (Database.GetPendingMigrations().Any())
			{
				Database.Migrate();
			}
			else
			{
				Database.EnsureCreated();
			}
		}
	}
}
