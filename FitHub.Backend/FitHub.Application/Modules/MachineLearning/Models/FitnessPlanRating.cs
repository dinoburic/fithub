using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.MachineLearning.Models
{
    public class FitnessPlanRating
    {
        [LoadColumn(0)] public float UserId { get; set; }
        [LoadColumn(1)] public float PlanId { get; set; }
        [LoadColumn(2)] public float Label { get; set; } 
    }
}
