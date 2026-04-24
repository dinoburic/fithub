using FitHub.Application.Modules.MachineLearning.Models;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.MachineLearning
{
    public class RecommendationModelBuilder
    {
        public void TrainAndSaveModel(string trainingDataPath, string modelSavePath)
        {
            MLContext mlContext = new MLContext();

            IDataView trainingData = mlContext.Data.LoadFromTextFile<FitnessPlanRating>(
                trainingDataPath, hasHeader: true, separatorChar: ',');

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(FitnessPlanRating.UserId),
                MatrixRowIndexColumnName = nameof(FitnessPlanRating.PlanId),
                LabelColumnName = nameof(FitnessPlanRating.Label),
                NumberOfIterations = 20,
                ApproximationRank = 100 
            };

            var trainerEstimator = mlContext.Recommendation().Trainers
                .MatrixFactorization(options);

            ITransformer model = trainerEstimator.Fit(trainingData);

            mlContext.Model.Save(model, trainingData.Schema, modelSavePath);
        }
    }
}
