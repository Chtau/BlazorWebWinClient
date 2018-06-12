using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebWinClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                // Add any custom services here
            });

            Terminal.Terminal.AddDefaults();

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
