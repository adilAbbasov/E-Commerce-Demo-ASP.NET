﻿using E_CommerceASP.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_CommerceASP.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<ProductTag> ProductTags { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId);

			modelBuilder.Entity<ProductTag>()
				.HasKey(pt => new { pt.ProductId, pt.TagId });

			modelBuilder.Entity<ProductTag>()
				.HasOne(pt => pt.Product)
				.WithMany(p => p.ProductTags)
				.HasForeignKey(pt => pt.ProductId);

			modelBuilder.Entity<ProductTag>()
				.HasOne(pt => pt.Tag)
				.WithMany(t => t.ProductTags)
				.HasForeignKey(pt => pt.TagId);
		}
	}
}
