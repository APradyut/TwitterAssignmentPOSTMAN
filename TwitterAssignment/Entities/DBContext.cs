using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAssignment.Entities
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options)
		{ }
		public DbSet<Users> Users { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Users>()
				.HasKey(a => a.Id);

			modelBuilder.Entity<Users>()
				.HasIndex(a => a.Username)	//Creating Username as  
				.IsUnique(true);
		}
	}
}
