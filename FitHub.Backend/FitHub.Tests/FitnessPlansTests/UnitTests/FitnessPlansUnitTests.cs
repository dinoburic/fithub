using FitHub.Application.Modules.Workout.FitnessPlans.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Tests.FitnessPlansTests.UnitTests
{
    public class FitnessPlanUnitTests
    {
        private DatabaseContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var fakeClock = new Microsoft.Extensions.Time.Testing.FakeTimeProvider();
            return new DatabaseContext(options, fakeClock);
        }

        [Fact]
        public async Task Handle_ShouldAddNewFitnessPlan()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var handler = new CreateFitnessPlanCommandHandler(context); 

            var command = new CreateFitnessPlanCommand
            {
                Title = "Generated Workout",
                Description = "Personalizovani plan",
                Difficulty = "Intermediate",
                Price = 49.99f,
                DailyDurationInMinutes = 45,
                DurationInWeeks = 4,
                CenterID = 1,
                FitnessPlanTypeID = 1,
                UserID = 1, 
                CreatedAtUtc = DateTime.UtcNow
            };

            // Act
            var resultId = await handler.Handle(command, CancellationToken.None);

            // Assert
            var plan = await context.FitnessPlans.FindAsync(resultId);

            Assert.NotNull(plan);
            Assert.Equal("Generated Workout", plan.Title);
            Assert.Equal("Intermediate", plan.Difficulty);
            Assert.Equal(49.99f, plan.Price);
            Assert.Equal(4, plan.DurationInWeeks);
            Assert.Equal(1, plan.CenterID);
        }
    }
}
