using SoilClassifier_Blazor.Models;

namespace SoilClassifier_Blazor.Shared
{
    public class ListState
    {
        public List<SoilSample> SoilSamples { get; set; } = new List<SoilSample>();
        public List<BoreHole> BoreHoleList { get; set; } = new List<BoreHole>();
        public List<GroupedSample> GroupedSamples { get; set; } = new List<GroupedSample>();
        public string? ProjectName { get; set; }
        public string? ProjectNumber { get; set; }
        public string? ReportNumber { get; set; }
        public string? ClientName { get; set; }
        public string? Limits { get; set; }
        public string? ChainageMeasuredFrom { get; set; }
        public string? SampledBy { get; set; }
        public string? SampledDate { get; set; }
        public string? PreparedBy { get; set; }
        public string? PreparedDate { get; set; }
        public string? Notes { get; set; }
        public string? SignatoryName { get; set; }
        public string? NataNumber { get; set; }
        public byte[]? NataLogo { get; set; }

    }
}
