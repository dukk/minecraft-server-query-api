using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<MineStatServerQueryService>();
        builder.Services.AddSingleton<IServerQueryService>((provider) => provider.GetRequiredService<MineStatServerQueryService>());

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            //app.UseExceptionHandler("/error");
            app.UseHsts();
        }

        app.MapControllers();
        app.Run();
    }
}