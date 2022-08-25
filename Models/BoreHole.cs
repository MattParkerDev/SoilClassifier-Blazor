namespace SoilClassifier_Blazor.Models
{
    public class BoreHole
    {
        public string? BoreNumber { get; set; }
        public string? Chainage { get; set; }
        public string? Offset { get; set; }
        public List<Layer>? LayerList { get; set; }
    }
}
