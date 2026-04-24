using FitHub.Application.Modules.MachineLearning.Models;
using FitHub.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class MLTrainingController(DatabaseContext ctx) : ControllerBase
    {
        [HttpPost("train")]
        public async Task<IActionResult> TrainModel()
        {
            var ratings = await ctx.Reviews
                .Where(r => !r.IsDeleted)
                .Select(r => new FitnessPlanRating
                {
                    UserId = r.UserID,
                    PlanId = r.FitnessPlanID,
                    Label = r.Rating
                })
                .ToListAsync();

            if (ratings.Count < 10)
                return BadRequest("You need at least 10 reviews!");

            MLContext mlContext = new MLContext();

            IDataView trainingData = mlContext.Data.LoadFromEnumerable(ratings);

            var dataProcessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "UserIdEncoded", inputColumnName: nameof(FitnessPlanRating.UserId))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "PlanIdEncoded", inputColumnName: nameof(FitnessPlanRating.PlanId)));

            var options = new Microsoft.ML.Trainers.MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "UserIdEncoded", 
                MatrixRowIndexColumnName = "PlanIdEncoded",    
                LabelColumnName = nameof(FitnessPlanRating.Label),
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainingPipeLine = dataProcessingPipeline.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            ITransformer model = trainingPipeLine.Fit(trainingData);

            mlContext.Model.Save(model, trainingData.Schema, "FitnessRecommender.zip");

            return Ok("Model trained and saved as FitnessRecommender.zip!");
        }
    }
}
