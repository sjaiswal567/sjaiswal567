using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthPen.Models
{
    public class PensionerDbContext :DbContext
    {
        public PensionerDbContext(DbContextOptions<PensionerDbContext> options) : base(options)
        { }
        public DbSet<PensionUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<PensionUser>()
                .HasData(
                new PensionUser
                { 
                    Id = 1,
                    UserName = "user1",
                    Password = "user1"
                },
                new PensionUser
                {
                    Id = 2,
                    UserName = "user2",
                    Password = "user2"
                },
                new PensionUser
                {
                    Id = 3,
                    UserName = "user3",
                    Password = "user3"
                }
                 );
        }

    }
}
   