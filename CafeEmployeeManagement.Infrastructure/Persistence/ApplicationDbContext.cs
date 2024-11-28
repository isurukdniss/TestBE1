﻿using CafeEmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeEmployeeManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee and Cafe relationships
            modelBuilder.Entity<Cafe>().HasMany(x => x.Employees).WithOne(x => x.Cafe)
                .HasForeignKey(x => x.CafeId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cafe>().HasData(
                    new Cafe
                    {
                        Id = new Guid("cb93d19a-4019-4770-b4f5-5787bd3e7da7"),
                        Name = "Brew Heaven",
                        Location = "123 Orchid Avenue, #05-67, Singapore 567890",
                        Description = "Coffe heaven",
                        Logo = "/img/brew_heaven.jpg",
                        CreatedDate = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedDate = new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Cafe
                    {
                        Id = new Guid("3e58d4dd-3d2a-422f-a4c0-e13758fdc7ef"),
                        Name = "Mocha Muse",
                        Location = "45 Merlion Lane, #12-34, Singapore 098765",
                        Description = "Amazing coffe cafe",
                        Logo = "/img/mocha_muse.jpg",
                        CreatedDate = new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedDate = new DateTime(2010, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    }
                );

            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = "UIAbcDEfg",
                        CafeId = new Guid("cb93d19a-4019-4770-b4f5-5787bd3e7da7"),
                        Name = "John Doe",
                        Email = "john.doe@example.com",
                        PhoneNumber = 98881111,
                        Gender = Domain.Enums.Gender.Male,
                        CreatedDate = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedDate = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Employee
                    {
                        Id = "UIArfDEfg",
                        CafeId = new Guid("cb93d19a-4019-4770-b4f5-5787bd3e7da7"),
                        Name = "Jane Doe",
                        Email = "jane.doe@example.com",
                        PhoneNumber = 98881221,
                        Gender = Domain.Enums.Gender.Female,
                        CreatedDate = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedDate = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Employee
                    {
                        Id = "UIBrfrEfg",
                        CafeId = new Guid("3e58d4dd-3d2a-422f-a4c0-e13758fdc7ef"),
                        Name = "Steve Smith",
                        Email = "steve.smith@example.com",
                        PhoneNumber = 88855221,
                        Gender = Domain.Enums.Gender.Male,
                        CreatedDate = new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedDate = new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    }
                );
        }
    }
}
