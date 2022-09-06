using CsvHelper.Configuration;

namespace SoilClassifier_Blazor.Models;

public class SoilSample
{
    public string? SampleID { get; set; }
    public int? SampleInteger { get; set; }
    public string? Chainage { get; set; }
    public string? BoreNumber { get; set; }
    public string? Offset { get; set; }
    public string? Depth { get; set; }
    public string? SurfaceType { get; set; }

    //Sieve Fractions
    public string? Sieve53 { get; set; }
    public string? Sieve19 { get; set; }
    public string? Sieve475 { get; set; }
    public string? Sieve236 { get; set; }
    public string? Sieve425 { get; set; }
    public string? Sieve075 { get; set; }

    public string? MoistureContent { get; set; }
    public string? PlasticLimit { get; set; }
    public string? PlasticityIndex { get; set; }
    public string? LinearShrinkage { get; set; }
    public string? LiquidLimit { get; set; }
    public string? WetPlasticityIndex { get; set; }

    public string? Colour { get; set; }
    public string? ClientName { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectNumber { get; set; }
    public string? SoilClassification { get; set; }
}

public sealed class SampleMap : ClassMap<SoilSample>
{
    public SampleMap()
    {

        Map(m => m.SampleID).Name("SampleID");
        Map(m => m.Chainage).Name("Chainage (m)");
        Map(m => m.BoreNumber).Name("Bore Hole No.");
        Map(m => m.Offset).Name("Offset");
        Map(m => m.Depth).Name("Depth Below Exst Surface (mm)");
        Map(m => m.SurfaceType).Name("Surface Type");
        Map(m => m.Sieve53).Name("53 mm");
        Map(m => m.Sieve19).Name("19 mm");
        Map(m => m.Sieve475).Name("4.75 mm");
        Map(m => m.Sieve236).Name("2.36 mm");
        Map(m => m.Sieve425).Name("0.425 mm");
        Map(m => m.Sieve075).Name("0.075 mm");
        Map(m => m.MoistureContent).Name("Moisture Content (%)");
        Map(m => m.PlasticLimit).Name("Plastic Limit (%)");
        Map(m => m.PlasticityIndex).Name("Plasticity Index (%)");
        Map(m => m.LinearShrinkage).Name("Linear Shrinkage ()");
        Map(m => m.LiquidLimit).Name("Liquid Limit (%)");
        Map(m => m.Colour).Name("Soil Classification");
        Map(m => m.ClientName).Name("ClientName");
        Map(m => m.ProjectName).Name("ProjectName");
        Map(m => m.ProjectNumber).Name("ProjectCode");

    }
}