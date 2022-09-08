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

        public List<int> GraphDepthLabels { get; set; } = new List<int>();
        public float GraphScalingUnit { get; set; }
        public float GraphMaxDepth { get; set; }

        public void GenerateDCPDepths()
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

        public void DepthLabelGenerator()
        {
            var tempList = new List<int>();

            float maxDepth = 0;
            foreach(var layer in LayerList)
            {
                if (layer.EndingDepth > maxDepth)
                {
                    maxDepth = layer.EndingDepth;
                }
            }
            if (maxDepth % 100 != 0)
            {
                //Round maxDepth up to next hundred
                maxDepth = (float)Math.Ceiling(maxDepth / 100) * 100;
            }

            int increment = 100;
            if (maxDepth > 1400)
            {
                maxDepth = (float)Math.Ceiling(maxDepth / 200) * 200;
                increment = 200;
            }
            if (maxDepth < 1000)
            {
                maxDepth = 1000;
            }

            for (int i = 0; i <= maxDepth; i += increment)
            {
                tempList.Add(i);
            }
            GraphDepthLabels = tempList;
            GraphMaxDepth = maxDepth;
            switch (maxDepth)
            {
                case 1000:
                    GraphScalingUnit = (float)increment / 28;
                    break;
                case 1100:
                    GraphScalingUnit = (float)increment / 26;
                    break;
                case 1200:
                    GraphScalingUnit = (float)increment / 26;
                    break;
                case 1300:
                    GraphScalingUnit = (float)increment / 24;
                    break;
                case 1400:
                    GraphScalingUnit = (float)increment / 24;
                    break;
                case 1600:
                    GraphScalingUnit = (float)increment / (float)35.7;
                    break;
                case 1800:
                    GraphScalingUnit = (float)increment / 29;
                    break;
                case 2000:
                    GraphScalingUnit = (float)increment / 28;
                    break;
                default:
                    break;
            }
        }
    }
}
