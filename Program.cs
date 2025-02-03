using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using G_UserInterface.Models;

namespace Accounting;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Gold Marketing API's", Version = "v1", Description = ".NET Core 8 Web API" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
           {
               {
                   new OpenApiSecurityScheme
                   {
                       Reference = new OpenApiReference
                       {
                           Type=ReferenceType.SecurityScheme,
                           Id="Bearer"
                       }
                   },
                   new string[]{}}
           });
        });

        builder.Services.AddDbContext<GUserInterfaceDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("G_UserInterface_DB"),
options => options.UseNodaTime()));


        builder.Services.AddProblemDetails();


        builder.Services.AddProblemDetails();

        WebApplication app = builder.Build();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();
        if (string.IsNullOrEmpty(env.WebRootPath))
        {
            env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

// Ensure wwwroot directory exists
        if (!Directory.Exists(env.WebRootPath))
        {
            Directory.CreateDirectory(env.WebRootPath);
        }
        app.UseStaticFiles();


        app.Run();
    }
}
