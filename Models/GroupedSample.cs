namespace SoilClassifier_Blazor.Models
{
    public class GroupedSample
    {
        public int? BaseSample { get; set; }
        public string? Classification { get; set; }
        public List<MatchedSample> MatchedSamples { get; set; } = new List<MatchedSample>();
    }

    public class MatchedSample
    {
        public int? SingleMatchedSample { get; set; }
    }
}
