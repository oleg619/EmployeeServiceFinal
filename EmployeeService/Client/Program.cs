using System;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeService.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var serverUrl = configuration.GetValue<string>("ServerUrl");
                return new HttpClient
                    {BaseAddress = new Uri(serverUrl)};
            });

            builder.Services.AddTransient<IEmployeeProvider, EmployeeProvider>();

            await builder.Build().RunAsync();
        }
    }
}