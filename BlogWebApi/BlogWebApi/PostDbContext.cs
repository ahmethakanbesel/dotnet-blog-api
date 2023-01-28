using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BlogWebApi.Models;

namespace BlogWebApi
{
	public class PostDbContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=PostDatabase.db", options =>
			{
				options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
			});
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Map table names
			modelBuilder.Entity<Post>().ToTable("Posts", "test");
			modelBuilder.Entity<Post>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Title);
				entity.Property(e => e.Category);
				entity.Property(e => e.Content);
				entity.Property(e => e.Author);
				entity.Property(e => e.CoverImage);
				entity.Property(e => e.CreateDate);
				entity.Property(e => e.UpdatedDate);
			});
			base.OnModelCreating(modelBuilder);
		}
	}
}