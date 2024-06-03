using AStar.ASPNet.Extensions.PipelineExtensions;
using AStar.ASPNet.Extensions.ServiceCollectionExtensions;
using AStar.ASPNet.Extensions.WebApplicationBuilderExtensions;
using AStar.FilesApi.StartupConfiguration;
using AStar.Logging.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

namespace AStar.FilesApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        try
        {
            _ = builder.CreateBootstrapLogger("astar-logging-settings.json")
                       .DisableServerHeader()
                       .AddLogging("astar-logging-settings.json");

            Log.Information("Starting {AppName}", typeof(Program).AssemblyQualifiedName);
            _ = builder.Services.ConfigureApi(new OpenApiInfo() { Title = "AStar Web Files API", Version = "v1" });
            _ = Services.Configure(builder.Services, builder.Configuration);

            var app = builder.Build()
                             .ConfigurePipeline();

            _ = ConfigurePipeline(app);

            app.Run();
        }
        catch(Exception ex)
        {
            Log.Error(ex, "Fatal error occurred in {AppName}", typeof(Program).AssemblyQualifiedName);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static WebApplication ConfigurePipeline(WebApplication app)
        // Additional configuration can be performed here
        => app;
}
