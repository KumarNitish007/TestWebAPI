using Microsoft.EntityFrameworkCore;
using TestWebAPI_Project.Models.Domain;

namespace TestWebAPI_Project.Data
{
	public class TestDbContext: DbContext
	{
        public TestDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
	}
}
