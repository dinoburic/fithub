using FitHub.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Tests.ReviewsTests.Integration
{
    public class ReviewIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ReviewIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.GetAuthenticatedClientAsync().Result;
        }

        [Fact]
        public async Task Post_CreateReview_ShouldReturnCreated_IfPurchased()
        {
            // Arrange
            var testPlanId = 1;

            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var currentUserService = scope.ServiceProvider.GetRequiredService<IAppCurrentUser>();
                var userId = currentUserService.UserId ?? 1; 

                db.Orders.Add(new Domain.Entities.Store.Orders
                {
                    UserID = userId,
                    IsDeleted = false,
                    Email = "test@fithub.com", 
                    FirstName = "Test",        
                    LastName = "Korisnik",
                    Items = new List<Domain.Entities.Store.OrderItems>
                {
                    new Domain.Entities.Store.OrderItems { FitnessPlanID = testPlanId }
                }
                });
                await db.SaveChangesAsync();
            }

            var request = new
            {
                Rating = 4,
                Comment = "Dobar trening za početnike.",
                FitnessPlanID = testPlanId
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Reviews", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
            Assert.NotNull(result);
            Assert.True(result.ContainsKey("id"));

            var reviewId = result["id"];
            Assert.NotEqual(0, reviewId);
        }
    }
}
