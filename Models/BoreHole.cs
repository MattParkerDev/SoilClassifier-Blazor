using FluentValidation;
using SoilClassifier_Blazor.Shared;
using System.ComponentModel.DataAnnotations;

namespace SoilClassifier_Blazor.Models
{
    public class BoreHole
    {
        public string? BoreNumber { get; set; }
        public string? Chainage { get; set; }
        public string? Offset { get; set; }
        public List<Layer> LayerList { get; set; } = new List<Layer>();
        public List<DCPLayer> DCPData { get; set; } = new List<DCPLayer>();

        [Required(ErrorMessage = "Enter a starting depth")]
        [Range(0, int.MaxValue, ErrorMessage = "Value must be >= 0")]
        public int? DCPStartingDepth { get; set; }

        [Required(ErrorMessage = "Enter an ending depth")]
        [Range(0, int.MaxValue, ErrorMessage = "Value must be >= 0")]
        public int? DCPEndingDepth { get; set; }

        public void GenerateDepths()
        {
            var tempList = new List<int>();

            var firstHundred = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(DCPStartingDepth) / 100.0) * 100);
            if (firstHundred != Convert.ToInt32(DCPStartingDepth))
            {
                tempList.Add(Convert.ToInt32(DCPStartingDepth));
            }

            var lastHundred = Convert.ToInt32(Math.Floor(Convert.ToDouble(DCPEndingDepth) / 100.0) * 100);
            for (int i = firstHundred; i <= lastHundred; i += 100)
            {
                tempList.Add(i);
            }
            if (lastHundred != Convert.ToInt32(DCPEndingDepth))
            {
                tempList.Add(Convert.ToInt32(DCPEndingDepth));
            }

            DCPData.Clear();
            foreach (var depth in tempList)
            {
                var tempLayer = new DCPLayer();
                tempLayer.Depth = depth;
                DCPData.Add(tempLayer);
            }
        }
    }
}
