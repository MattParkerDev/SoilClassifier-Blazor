using CsvHelper.Configuration;

namespace SoilClassifier_Blazor.Models;

public class SoilSample
{
    public string? SampleID { get; set; }
    public string? Chainage { get; set; }
    public string? Offset { get; set; }
    public string? Depth { get; set; }
    public string? SurfaceType { get; set; }

    //Sieve Fractions
    public int Sieve53 { get; set; }
    public int Sieve19 { get; set; }
    public int Sieve475 { get; set; }
    public int Sieve236 { get; set; }
    public int Sieve425 { get; set; }
    public int Sieve075 { get; set; }

    public double MoistureContent { get; set; }
    public double PlasticLimit { get; set; }
    public double PlasticityIndex { get; set; }
    public double LinearShrinkage { get; set; }
    public double LiquidLimit { get; set; }

    public string? Colour { get; set; }
    public string? ClientName { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectNumber { get; set; }
}

public sealed class SampleMap: ClassMap<SoilSample>
{
    public SampleMap()
    {
        // TODO: Add all maps
        Map(m => m.SampleID).Name("SampleID");
    }
}