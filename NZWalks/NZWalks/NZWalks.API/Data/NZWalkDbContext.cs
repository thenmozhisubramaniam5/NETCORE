using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalkDbContext:DbContext
    {
        public NZWalkDbContext(DbContextOptions<NZWalkDbContext> dbContextOptions) : base(dbContextOptions) 
        {
                        
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Images> Images_Info { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for Difficulties
            // Easy, Medium and hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("b7aadb18-67bb-4948-bffe-0e43452b1fe5"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("94354fc7-2798-4985-803d-325e4ab03797"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("867102b8-6f1f-4d66-8362-0831c7476b8d"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // seed data for region
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id= Guid.Parse("4ac91331-a755-45bc-b53b-27454f5ab968"),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "Test123.jpg"
                },
                new Region()
                {
                    Id= Guid.Parse("311adc75-a217-43d3-a762-73e367185a6c"),
                    Name = "GreenLand",
                    Code = "GRL",
                    RegionImageUrl = "Green123.jpg"
                },
                new Region()
                {
                    Id= Guid.Parse("8f9d51b4-58b7-4b67-90bb-7f402ef2beb7"),
                    Name = "South Africa Region",
                    Code = "SAR",
                    RegionImageUrl = "SARegion.jpg"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
