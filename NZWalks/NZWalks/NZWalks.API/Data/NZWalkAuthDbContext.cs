using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalkAuthDbContext : IdentityDbContext
    {
        public NZWalkAuthDbContext(DbContextOptions<NZWalkAuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerGuid = "c48c6993-4af0-4eb0-9e51-9dc5b9f575a0";
            var writerGuid = "3df64e44-e3bc-4d02-b0be-534848bad4c1";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerGuid,
                    ConcurrencyStamp = readerGuid,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerGuid,
                    ConcurrencyStamp = writerGuid,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

