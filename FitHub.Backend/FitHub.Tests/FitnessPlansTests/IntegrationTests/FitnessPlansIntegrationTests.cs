using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Tests.FitnessPlansTests.IntegrationTests
{
    public class FitnessPlanIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FitnessPlanIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.GetAuthenticatedClientAsync().Result;
        }

        [Fact]
        public async Task Post_CreateFitnessPlan_ShouldReturnCreated()
        {
            // Arrange
            var request = new
            {
                Title = "Integration Test Plan",
                Description = "Testiranje HTTP pipeline-a",
                Difficulty = "Beginner",
                Price = 29.99f,
                DailyDurationInMinutes = 30,
                DurationInWeeks = 6,
                CenterID = 1,
                FitnessPlanTypeID = 1,
                UserID = 1,
                CreatedAtUtc = DateTime.UtcNow
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/FitnessPlans", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
            Assert.NotNull(result);
            Assert.True(result.ContainsKey("id")); 

            var planId = result["id"];
            Assert.NotEqual(0, planId);
        }
    }
}
