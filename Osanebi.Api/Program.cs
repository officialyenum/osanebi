using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Osanebi.DataAccess;
using Osanebi.Model.IdentityModels;
using Osanebi.Service;
using Osanebi.Service.IService;
using Osanebi.Utility;
using Osanebi.Utility.IUtility;
using System.Diagnostics;
using System.Text;
using TokenHandler = Osanebi.Utility.TokenHandler;

namespace Osanebi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Osanebi API",
                    Description = "An ASP.NET Core Web API for Osanebi application.",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header usin the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Autorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
            builder.Services.AddSingleton<IApplicationEmailSender, ApplicationEmailSender>();
            builder.Services.Configure<TokenConfiguration>(builder.Configuration.GetSection("TokenConfiguration"));
            builder.Services.AddSingleton<ITokenHandler, TokenHandler>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o => 
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
                        ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenConfiguration:SecretKey"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    }
                );


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Osanebi API V1");
                    c.RoutePrefix = "documentation"; // Set Swagger UI at the /documentation
                    //c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });

                var url = "https://localhost:7070/documentation";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
