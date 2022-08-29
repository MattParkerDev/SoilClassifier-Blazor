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
        public List<int> DCPDepths { get; set; } = new List<int>();
        [Required]
        public int? DCPStartingDepth { get; set; }
        [Required]
        public int? DCPEndingDepth { get; set; }
    }
}
