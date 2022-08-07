using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using SoilClassifier_Blazor;

namespace SoilClassifier_Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
    public class SoilData
    {
        public double? finePercent { get; set; }

        public double? gravelPercent { get; set; }

        public double? plasticLimit { get; set; }

        public double? liquidLimit { get; set; }

    }
    public class SoilDataValidator : AbstractValidator<SoilData>
    {
        public SoilDataValidator()
        {
            RuleSet("Numbers", () =>
            {
                RuleFor(soilData => soilData.finePercent)
                .NotEmpty().WithMessage("This field is required.")
                .LessThanOrEqualTo(100).WithMessage("Enter a number from 0-100")
                .GreaterThanOrEqualTo(0).WithMessage("Enter a number from 0-100")
                ;

                RuleFor(soilData => soilData.gravelPercent)
                .NotEmpty().WithMessage("This field is required.")
                .LessThanOrEqualTo(100).WithMessage("Enter a number from 0-100")
                .GreaterThanOrEqualTo(0).WithMessage("Enter a number from 0-100")
                .LessThanOrEqualTo(soilData => 100 - soilData.finePercent)
                .WithMessage("Fines + Gravel must be <= 100");

                RuleFor(soilData => soilData.plasticLimit)
                .NotEmpty().WithMessage("This field is required.")
                .LessThanOrEqualTo(100).WithMessage("Enter a number from 0-100")
                .GreaterThanOrEqualTo(0).WithMessage("Enter a number from 0-100");


                RuleFor(soilData => soilData.liquidLimit)
                .NotEmpty().WithMessage("This field is required.")
                .LessThanOrEqualTo(100).WithMessage("Enter a number from 0-100")
                .GreaterThanOrEqualTo(0).WithMessage("Enter a number from 0-100")
                .GreaterThan(soilData => soilData.plasticLimit)
                .WithMessage("Liquid Limit must be greater than Plastic Limit");

            });
            
        }
    }
}