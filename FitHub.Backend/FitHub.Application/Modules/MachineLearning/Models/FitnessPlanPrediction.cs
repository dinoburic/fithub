using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.MachineLearning.Models
{
    public class FitnessPlanPrediction
    {
        public float Label { get; set; } // Stvarna ocjena 
        public float Score { get; set; } // Predicted rating the user would give
    }
}
