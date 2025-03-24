using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Comment>().HasOne(x=>x.CommentMaker).WithMany().HasForeignKey(x=>x.CommentMakerId).OnDelete(DeleteBehavior.NoAction);
		}
	}
}
