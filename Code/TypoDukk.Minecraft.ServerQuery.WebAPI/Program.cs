using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("./networkquery.json", true, true);
        var section = builder.Configuration.GetSection("NetworkQuery");
        var queryNetworkConfigurationSection = section.Get<QueryNetworkConfigurationSection>();

        if (queryNetworkConfigurationSection is not null)
            builder.Services.AddSingleton(queryNetworkConfigurationSection);

        builder.Services.AddSingleton<MineStatServerQueryService>();
        builder.Services.AddSingleton<IServerQueryService>((provider) => provider.GetRequiredService<MineStatServerQueryService>());
        builder.Services.AddSingleton<QueryNetworkService>();
        builder.Services.AddSingleton<IQueryNetworkService>((provider) => provider.GetRequiredService<QueryNetworkService>());

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Minecraft Server Query API",
                Description = "Exposes a REST API to query minecraft servers status. Designed for use on https://minecraft.dukk.org/ but may be applicable to others.",
                Contact = new OpenApiContact()
                {
                    Name = "dukk",
                    Url = new Uri("https://github.com/dukk/minecraft-server-query-api")
                }
            });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, 
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.MapControllers();
        app.Run();
    }
}