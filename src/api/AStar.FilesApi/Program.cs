using AStar.ASPNet.Extensions.PipelineExtensions;
using AStar.ASPNet.Extensions.ServiceCollectionExtensions;
using AStar.ASPNet.Extensions.WebApplicationBuilderExtensions;
using AStar.FilesApi.StartupConfiguration;
using AStar.Infrastructure.Models;
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

#pragma warning disable S1075 // URIs should not be hardcoded
            var filesAsString = System.IO.File.ReadAllText(@"F:\repos\astar-filesapi\tests\unit\AStar.FilesApi.Unit.Tests\TestFiles\files.json");
            var filesAsObjects = System.Text.Json.JsonSerializer.Deserialize<IList<FileDetail>>(filesAsString);
            System.IO.File.WriteAllText(@"F:\repos\astar-filesapi\tests\unit\AStar.FilesApi.Unit.Tests\TestFiles\files.json", System.Text.Json.JsonSerializer.Serialize(filesAsObjects));
#pragma warning restore S1075 // URIs should not be hardcoded
            
            foreach (var file in filesAsObjects!.Where(fd=>fd.DirectoryName == "c:\\temp\\Famous\\coats").OrderBy(file=> file.FullNameWithPath))
            {
                Console.WriteLine(file.FullNameWithPath);
            }

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
