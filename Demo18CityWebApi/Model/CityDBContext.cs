using System;
using Microsoft.EntityFrameworkCore;

namespace Demo18CityWebApi.Model
{
	public class CityDBContext:DbContext
    {      
		
		public DbSet<CityDto> Cities { get; set; }

		public CityDBContext(DbContextOptions<CityDBContext> options) : base(options)
		{
			Database.EnsureCreated();
			//Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder){
			modelBuilder.Entity<CityDto>().Property(a => a.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<CityDto>().HasKey(a=>a.Id);         
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			var connectionString = "server=localhost;userid=root;pwd=please;port=3306;database=lms;sslmode=none";
            optionsBuilder.UseMySQL(connectionString);
            base.OnConfiguring(optionsBuilder);
		}

    }
}
