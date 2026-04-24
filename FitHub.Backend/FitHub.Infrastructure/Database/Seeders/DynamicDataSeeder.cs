using FitHub.Domain.Entities.Catalog;
using FitHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FitHub.Domain.Entities.Identity;
using FitHub.Domain.Entities.Training;
namespace FitHub.Infrastructure.Database.Seeders;

/// <summary>
/// Dynamic seeder that runs at runtime,
/// usually during application startup (e.g., in Program.cs).
/// Koristi se za unos demo/test podataka koji nisu dio migracije.
/// </summary>
public static class DynamicDataSeeder
{
    public static async Task SeedAsync(DatabaseContext context)
    {
        await context.Database.EnsureCreatedAsync();

        await SeedFitnessCentersAsync(context);
        await SeedFitnessPlanTypes(context);
        await SeedUserRoles(context);
       
        // await SeedProductCategoriesAsync(context);
        await SeedUsersAsync(context);
        await SeedFitnessPlansAsync(context);
        await SeedReviewsAsync(context);
    }

    private static async Task SeedFitnessPlanTypes(DatabaseContext context)
    {
        if (!await context.FitnessPlanTypes.AnyAsync())
        {
            context.FitnessPlanTypes.AddRange(
                new FitnessPlanTypes
                {
                    Title="Cardio Endurance"
                },
                new FitnessPlanTypes
                {
                    Title="Strength Training"
                },
                new FitnessPlanTypes
                {
                    Title = "Flexibility & Mobility"
                },
                new FitnessPlanTypes
                {
                    Title = "Weight Loss Program"
                },
                new FitnessPlanTypes
                {
                    Title = "Muscle Gain Program"
                }
                );

            context.FitnessPlanTypes.AddRange();
            await context.SaveChangesAsync();

             
        }
        else
        {
             
        }
    }

    private static async Task SeedFitnessCentersAsync(DatabaseContext context)
    {
        if (!await context.FitnessCenters.AnyAsync())
        {
            context.FitnessCenters.AddRange(
                new FitnessCenters
                {
                    CenterID = 1,
                    Name = "Arena fitness centar",
                    Location = "Mostar, BH",
                    Capacity = 50,
                    IsDeleted =false
                },
                new FitnessCenters
                {
                    CenterID = 2,
                    Name = "Perfect Fit Gym & Fitness",
                    Location = "Mostar, BH",
                    Capacity = 100,
                    IsDeleted = false
                }
                );


            context.FitnessCenters.AddRange();
            await context.SaveChangesAsync();

             
        }
        else
        {
             
        }
    }

    private static async Task SeedUserRoles(DatabaseContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            context.Roles.AddRange(
                new Roles
                {
                    Name="User"
                },
                new Roles
                {
                    Name = "Admin"
                },
                new Roles
                {
                    Name = "Coach"
                }
                );

            context.Roles.AddRange();
            await context.SaveChangesAsync();

             
        }
        else
        {
             
        }
    }


    /// <summary>
    /// Creates demo users if they do not already exist in the database.
    /// Note: this method previously used MarketUserEntity and the password hasher for that type.
    /// If you want to seed application users (domain `Users`) instead, implement separate seeding
    /// that ensures Roles and FitnessPlanTypes exist first and then create Users referencing those values.
    /// </summary>
    private static async Task SeedUsersAsync(DatabaseContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var hasher = new PasswordHasher<MarketUserEntity>();

        var users = new List<Users>
        {
            new Users { Email = "string@string.com", PasswordHash = hasher.HashPassword(null!, "@String123"), Name="String", Surname="String", RoleID = 1, CenterID=1, Gender=true, IsDeleted=false, Status="Active" },
            new Users { Email = "marko@test.com", PasswordHash = hasher.HashPassword(null!, "Test1234!"), Name="Marko", Surname="Marić", RoleID = 1, CenterID=1, Gender=true, IsDeleted=false, Status="Active" },
            new Users { Email = "ana@test.com", PasswordHash = hasher.HashPassword(null!, "Test1234!"), Name="Ana", Surname="Anić", RoleID = 1, CenterID=2, Gender=false, IsDeleted=false, Status="Active" },
            new Users { Email = "ivan@test.com", PasswordHash = hasher.HashPassword(null!, "Test1234!"), Name="Ivan", Surname="Ivanić", RoleID = 1, CenterID=1, Gender=true, IsDeleted=false, Status="Active" },
            new Users { Email = "jasmina@test.com", PasswordHash = hasher.HashPassword(null!, "Test1234!"), Name="Jasmina", Surname="Jasić", RoleID = 1, CenterID=2, Gender=false, IsDeleted=false, Status="Active" }
        };

        context.Set<Users>().AddRange(users);
        await context.SaveChangesAsync();

         
    }

    private static async Task SeedReviewsAsync(DatabaseContext context)
    {
        if (await context.Reviews.AnyAsync())
        {
             
            return;
        }

        var users = await context.Users.Select(u => u.UserID).ToListAsync();

        var plans = await context.FitnessPlans
            .Select(p => new { p.PlanID, p.CenterID })
            .ToListAsync();

        if (!users.Any() || !plans.Any()) return;

        var reviews = new List<Reviews>();
        var random = new Random();

        foreach (var userId in users)
        {
            var userPlans = plans.OrderBy(x => random.Next()).Take(8).ToList();

            foreach (var plan in userPlans)
            {
                int rating = random.Next(3, 6);

                reviews.Add(new Reviews
                {
                    UserID = userId,
                    FitnessPlanID = plan.PlanID,
                    CenterID = plan.CenterID,
                    Rating = rating,
                    Comment = $"Ovo je automatski generisana recenzija sa ocjenom {rating}.",
                    CreatedAtUTC = DateTime.UtcNow,
                    IsDeleted = false
                });
            }
        }

        context.Reviews.AddRange(reviews);
        await context.SaveChangesAsync();

         
    }
    private static async Task SeedFitnessPlansAsync(DatabaseContext context)
    {
        if (!await context.FitnessPlans.AnyAsync())
        {
            var plans = new List<FitnessPlans>
        {
            // ==========================================================
            // CENTER 1: Arena fitness centar (CenterID = 1)
            // ==========================================================
            new FitnessPlans { Title = "Ultimate Fat Burner", Description = "High-intensity cardio program designed to maximize calorie burn.", Difficulty = "Intermediate", Price = 29.99f, DailyDurationInMinutes = 45, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 4, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Advanced Muscle Mass", Description = "Hypertrophy focused program for experienced lifters.", Difficulty = "Advanced", Price = 49.99f, DailyDurationInMinutes = 60, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 5, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Marathon Prep Protocol", Description = "Intensive long-distance running preparation.", Difficulty = "Advanced", Price = 39.99f, DailyDurationInMinutes = 60, DurationInWeeks = 12, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "HIIT Cardio Blast", Description = "Short but intense intervals to skyrocket your metabolism.", Difficulty = "Intermediate", Price = 19.99f, DailyDurationInMinutes = 20, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Powerlifting Basics", Description = "Learn the big three: Squat, Bench, and Deadlift.", Difficulty = "Beginner", Price = 34.99f, DailyDurationInMinutes = 90, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Core Crusher", Description = "Build a solid foundation and visible abs in just weeks.", Difficulty = "Intermediate", Price = 14.99f, DailyDurationInMinutes = 15, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Yoga for Lifters", Description = "Increase your mobility and prevent injuries with targeted yoga.", Difficulty = "Beginner", Price = 24.99f, DailyDurationInMinutes = 30, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "30-Day Shred", Description = "A daily challenge to shed maximum weight in one month.", Difficulty = "Advanced", Price = 44.99f, DailyDurationInMinutes = 40, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 4, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Bulking Season", Description = "Eat big, lift big. A complete guide to gaining size.", Difficulty = "Intermediate", Price = 39.99f, DailyDurationInMinutes = 75, DurationInWeeks = 12, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 5, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Swimming Endurance", Description = "Pool-based workouts to build lung capacity and stamina.", Difficulty = "Intermediate", Price = 19.99f, DailyDurationInMinutes = 45, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Calisthenics Mastery", Description = "Master your bodyweight with pullups, dips, and muscle-ups.", Difficulty = "Advanced", Price = 29.99f, DailyDurationInMinutes = 60, DurationInWeeks = 10, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Posture Correction", Description = "Fix rounded shoulders and anterior pelvic tilt.", Difficulty = "Beginner", Price = 15.00f, DailyDurationInMinutes = 20, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Kettlebell Inferno", Description = "Dynamic kettlebell flows for fat loss and power.", Difficulty = "Intermediate", Price = 24.99f, DailyDurationInMinutes = 35, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 4, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Glute Builder", Description = "Targeted exercises for lower body strength and aesthetics.", Difficulty = "Intermediate", Price = 29.99f, DailyDurationInMinutes = 50, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 5, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Couch to 5K", Description = "The ultimate beginner running program.", Difficulty = "Beginner", Price = 9.99f, DailyDurationInMinutes = 30, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Olympic Weightlifting", Description = "Snatch and Clean & Jerk techniques for athletes.", Difficulty = "Advanced", Price = 59.99f, DailyDurationInMinutes = 90, DurationInWeeks = 12, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Active Recovery", Description = "Light movements to perform on your rest days.", Difficulty = "Beginner", Price = 12.99f, DailyDurationInMinutes = 20, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Low Impact Fat Loss", Description = "Joint-friendly exercises for sustainable weight loss.", Difficulty = "Beginner", Price = 19.99f, DailyDurationInMinutes = 40, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 4, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Lean Muscle Protocol", Description = "Gain muscle without the extra fat.", Difficulty = "Intermediate", Price = 34.99f, DailyDurationInMinutes = 60, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 5, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Iron Man Training", Description = "Swim, bike, and run. Prepare for the ultimate triathlon.", Difficulty = "Advanced", Price = 69.99f, DailyDurationInMinutes = 120, DurationInWeeks = 16, ReviewsNumber = 0, AverageRating = 0f, CenterID = 1, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },

            // ==========================================================
            // CENTER 2: Perfect Fit Gym & Fitness (CenterID = 2)
            // ==========================================================
            new FitnessPlans { Title = "Strength Foundations", Description = "Learn the basics of weightlifting and form.", Difficulty = "Beginner", Price = 19.99f, DailyDurationInMinutes = 30, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Morning Mobility & Yoga", Description = "Dynamic stretching and yoga flows for flexibility.", Difficulty = "Beginner", Price = 15.00f, DailyDurationInMinutes = 20, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Sprint Interval Training", Description = "Explosive sprints to build speed and stamina.", Difficulty = "Advanced", Price = 24.99f, DailyDurationInMinutes = 25, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Upper Body Sculpt", Description = "Focus on chest, back, and arms definition.", Difficulty = "Intermediate", Price = 22.99f, DailyDurationInMinutes = 45, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Pilates Reformer At Home", Description = "Core stability and control using just a mat.", Difficulty = "Intermediate", Price = 29.99f, DailyDurationInMinutes = 40, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Keto Weight Loss", Description = "Workout routines designed to pair with a ketogenic diet.", Difficulty = "Intermediate", Price = 34.99f, DailyDurationInMinutes = 50, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 4, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Dumbbell Only Gains", Description = "Build mass at home using only a pair of dumbbells.", Difficulty = "Intermediate", Price = 19.99f, DailyDurationInMinutes = 45, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 5, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Rowing Endurance", Description = "Ergometer workouts for full-body cardiovascular health.", Difficulty = "Intermediate", Price = 14.99f, DailyDurationInMinutes = 30, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Functional Fitness", Description = "Train your body for daily activities and overall wellness.", Difficulty = "Beginner", Price = 18.99f, DailyDurationInMinutes = 35, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Stretching for Desk Workers", Description = "Relieve tension from sitting all day.", Difficulty = "Beginner", Price = 9.99f, DailyDurationInMinutes = 15, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Summer Slimdown", Description = "Get beach-ready with this high-paced fat burning routine.", Difficulty = "Intermediate", Price = 29.99f, DailyDurationInMinutes = 45, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 4, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Bodybuilder Split", Description = "Classic Bro-Split (Chest/Back/Legs/Arms/Shoulders).", Difficulty = "Advanced", Price = 39.99f, DailyDurationInMinutes = 60, DurationInWeeks = 12, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 5, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Trail Running Prep", Description = "Prepare your ankles and lungs for off-road running.", Difficulty = "Intermediate", Price = 24.99f, DailyDurationInMinutes = 50, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Strongman Basics", Description = "Heavy carries, holds, and odd-object lifting.", Difficulty = "Advanced", Price = 44.99f, DailyDurationInMinutes = 75, DurationInWeeks = 8, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Gymnastics Rings", Description = "Build incredible core strength with ring exercises.", Difficulty = "Advanced", Price = 34.99f, DailyDurationInMinutes = 45, DurationInWeeks = 10, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Extreme Weight Cut", Description = "Short-term aggressive protocol for combat sports weigh-ins.", Difficulty = "Advanced", Price = 49.99f, DailyDurationInMinutes = 90, DurationInWeeks = 2, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 4, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Arm Size Builder", Description = "Specialized block to add inches to your biceps and triceps.", Difficulty = "Intermediate", Price = 19.99f, DailyDurationInMinutes = 40, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 5, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Jump Rope Mastery", Description = "Cardio conditioning utilizing only a jump rope.", Difficulty = "Beginner", Price = 12.99f, DailyDurationInMinutes = 20, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 1, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "TRX Suspension Training", Description = "Full body workouts using TRX straps.", Difficulty = "Intermediate", Price = 29.99f, DailyDurationInMinutes = 45, DurationInWeeks = 6, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 2, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
            new FitnessPlans { Title = "Deep Tissue Release", Description = "Foam rolling and myofascial release techniques.", Difficulty = "Beginner", Price = 14.99f, DailyDurationInMinutes = 20, DurationInWeeks = 4, ReviewsNumber = 0, AverageRating = 0f, CenterID = 2, FitnessPlanTypeID = 3, UserID = 1, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow }
        };

            context.FitnessPlans.AddRange(plans);
            await context.SaveChangesAsync();

             
        }
        else
        {
             
        }
    }
}
