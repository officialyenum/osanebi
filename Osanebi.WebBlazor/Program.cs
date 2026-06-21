using Microsoft.AspNetCore.Localization;
using MudBlazor.Services;
using Osanebi.WebBlazor.Components;
using Osanebi.WebBlazor.Service;
using System.Globalization;




namespace Osanebi.WebBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var apiBaseAddress = builder.Configuration["ApiBaseAddress"] ?? "https://localhost:7070/api/";


            // Add services to the container.

            builder.Services.AddLocalization();
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"), // English (United States)
                new CultureInfo("en-GB"), // English (United Kingdom)

                new CultureInfo("fr-FR"), // French (France)
                new CultureInfo("de-DE"), // German (Germany)
                new CultureInfo("nl-NL"), // Dutch (Netherlands)

                new CultureInfo("es-ES"), // Spanish (Spain)

                new CultureInfo("zh-CN"), // Mandarin Chinese (Simplified, China)

                new CultureInfo("ig-NG"), // Igbo (Nigeria)
                new CultureInfo("yo-NG")  // Yoruba (Nigeria)
            };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-GB");

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();


            // Register the custom services
            builder.Services.AddMudServices();
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBaseAddress)
            });
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
