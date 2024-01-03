using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyWebApi;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.RegisterApplicationServices();
        
        var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

        var app = builder.Build();

        app.RegisterMiddlewares();


        app.MapGet("/", () => "Todos tutorial from MS");
        app.ConfigureServices();

        app.Run($"http://localhost:{port}");
    }
}