using System.IO.Abstractions;
using AStar.ASPNet.Extensions.PipelineExtensions;
using AStar.ASPNet.Extensions.ServiceCollectionExtensions;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace AStar.FilesApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        _ = builder.AddLogging();
        _ = builder.Services.ConfigurePipeline();
        var configuration = builder.Configuration;
        var config = configuration.GetDebugView();
        Console.WriteLine(config);
        _ = ConfigureServices(builder.Services);

        var app = builder.Build();
        _ = app.ConfigurePipeline();
        _ = ConfigurePipeline(app);
        app.Run();
    }

    private static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        _ = services.AddDbContext<FilesContext>();
        _ = services.AddSwaggerGenNewtonsoftSupport();
        _ = services.AddSingleton<IFileSystem, FileSystem>();

        return services;
    }

    private static WebApplication ConfigurePipeline(WebApplication app)
        // Additional configuration can be performed here
        => app;
}
