using BlogApp.Data.Configurations;
using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
	public class ApplicationDbContext:IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			Database.Migrate();
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration<Article>(new ArticleConfiguration());
			builder.ApplyConfiguration<Tag>(new TagConfiguration());
			builder.ApplyConfiguration<Comment>(new CommentConfiguration());
			builder.ApplyConfiguration<Role>(new RoleConfiguration());
		}
	}
}
