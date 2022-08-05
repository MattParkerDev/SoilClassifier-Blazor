using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "This field is required")]
        [Range(0,100, ErrorMessage ="Please enter a number between 0 and 100")]
        public string finePercent { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
        public string gravelPercent { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
        public string plasticLimit { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
        public string liquidLimit { get; set; }


    }
}