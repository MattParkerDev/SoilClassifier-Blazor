namespace SoilClassifier_Blazor.Models
{
    public class Layer
    {
        public string GraphColor { get; set; } = "";
        public float Height { get; set; }
        public float StartingDepth { get; set; }
        public float EndingDepth { get; set; }
        public string? SurfaceType { get; set; }
        public string? MoistureContent { get; set; }
        public string? SoilColor { get; set; }
        public string? SoilClassification { get; set; }

    }
}
