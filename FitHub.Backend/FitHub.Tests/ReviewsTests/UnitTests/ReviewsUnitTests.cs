using FitHub.Application.Abstractions;
using FitHub.Application.Modules.Reviews.Commands.Create;
using FitHub.Infrastructure.Database; // Adjust the path to your actual DatabaseContext if it differs
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Tests.ReviewsTests.UnitTests
{
    public class ReviewUnitTests
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
        public async Task Handle_ShouldAddNewReview_WhenUserHasPurchasedPlan()
        {
            // Arrange
            using var context = GetInMemoryDbContext();

            var mockUser = new Mock<IAppCurrentUser>();
            mockUser.Setup(u => u.UserId).Returns(10);
            mockUser.Setup(u => u.CenterId).Returns(1);

            context.Orders.Add(new Domain.Entities.Store.Orders
            {
                UserID = 10,
                Email = "test@fithub.com", 
                FirstName = "Test",       
                LastName = "Korisnik",
                IsDeleted = false,
                Items = new List<Domain.Entities.Store.OrderItems>
            {
                new Domain.Entities.Store.OrderItems { FitnessPlanID = 1 }
            }
            });
            await context.SaveChangesAsync();

            var handler = new CreateReviewCommandHandler(context, mockUser.Object);

            var command = new CreateReviewCommand
            {
                Rating = 5,
                Comment = "Odličan plan, puno mi je pomogao!",
                FitnessPlanID = 1
            };

            // Act
            var resultId = await handler.Handle(command, CancellationToken.None);

            // Assert
            var review = await context.Reviews.FindAsync(resultId);

            Assert.NotNull(review);
            Assert.Equal(5, review.Rating);
            Assert.Equal(10, review.UserID); // Verify it took the ID from the mocked user
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserHasNotPurchasedPlan()
        {
            // Arrange
            using var context = GetInMemoryDbContext();

            var mockUser = new Mock<IAppCurrentUser>();
            mockUser.Setup(u => u.UserId).Returns(10);
            mockUser.Setup(u => u.CenterId).Returns(1); // Added to resolve item 29 inside this test

            var handler = new CreateReviewCommandHandler(context, mockUser.Object);
            var command = new CreateReviewCommand { Rating = 5, FitnessPlanID = 1 };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(command, CancellationToken.None));
            
         }
    }
}
