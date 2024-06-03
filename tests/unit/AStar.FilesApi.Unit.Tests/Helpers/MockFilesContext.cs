using System.Text.Json;
using AStar.Infrastructure.Data;
using AStar.Web.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AStar.FilesApi.Helpers;

public class MockFilesContext : IDisposable
{
    private readonly SqliteConnection connection;
    private readonly DbContextOptions<FilesContext> contextOptions;
    private bool disposedValue;

    public MockFilesContext()
    {
        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
        // at the end of the test (see Dispose below).
        connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        // These options will be used by the context instances in this test suite, including the connection opened above.
        contextOptions = new DbContextOptionsBuilder<FilesContext>()
            .UseSqlite(connection)
            .Options;

        // Create the schema and seed some data
        using var context = new FilesContext(contextOptions);

        _ = context.Database.EnsureCreated();

        AddMockFiles(context);
        _ = context.SaveChanges();
    }

    public FilesContext CreateContext() => new(contextOptions);

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(!disposedValue)
        {
            if(disposing)
            {
                connection.Dispose();
            }

            disposedValue = true;
        }
    }

    private static void AddMockFiles(FilesContext mockFilesContext)
    {
        var filesAsJson = File.ReadAllText(@"TestFiles\files.json");

        var listFromJson = JsonSerializer.Deserialize<IEnumerable<FileDetail>>(filesAsJson)!;

        foreach (var item in listFromJson)
        {
            if (mockFilesContext.Files.FirstOrDefault(f => f.FileName == item.FileName && f.DirectoryName == item.DirectoryName) == null)
            {
                Console.WriteLine($"About to add {item.FullNameWithPath}");
                mockFilesContext.Files.Add(item);
                mockFilesContext.SaveChanges();
            }
        }
    }
}
