using FitHub.Domain.Entities.Catalog;
using FitHub.Domain.Entities.Identity;
using FitHub.Domain.Entities.Training;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Infrastructure.Database.Seeders;

public partial class StaticDataSeeder
{
    private static DateTime DateTime { get; set; } = new DateTime(2022, 4, 13, 1, 22, 18, 866, DateTimeKind.Local);

    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedFitnessCenters(modelBuilder);
        // SeedProductCategories(modelBuilder);
    }

    private static void SeedProductCategories(ModelBuilder modelBuilder)
    {
        // todo: user roles

        //modelBuilder.Entity<UserRoles>().HasData(new List<UserRoleEntity>
        //{
        //    new UserRoleEntity{
        //        Id = 1,
        //        Name = "Admin",
        //        CreatedAt = dateTime,
        //        ModifiedAt = null,
        //    },
        //    new UserRoleEntity{
        //        Id = 2,
        //        Name = "Employee",
        //        CreatedAt = dateTime,
        //        ModifiedAt = null,
        //    },
        //});
    }

    private static void SeedFitnessCenters(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FitnessCenters>().HasData(
            new FitnessCenters { CenterID=1,Name = "Arena sport center", Location = "Mostar, BH", Capacity = 200, IsDeleted = false },
            new FitnessCenters { CenterID=2,Name = "Perfect Fit Gym & Fitness", Location = "Mostar, BH", Capacity = 120, IsDeleted = false }
            //new FitnessCenters { CenterID = 3, Name = "FitHub West", Location = "Rijeka, HR", Capacity = 150 }
        );
    }
}