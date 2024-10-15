using System.Text.Json;
using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;

namespace AStar.FilesApi.Helpers;

public class MockFilesContext : IDisposable
{
    private static readonly FilesContext Context = new(new() { Value = "Filename=:memory:" }, new() { InMemory = true });

    private bool disposedValue;

    public MockFilesContext()
    {
        _ = Context.Database.EnsureCreated();
        Console.WriteLine(Context.Files.Count());
        AddMockFiles(Context);
        _ = Context.SaveChanges();
    }

    public static FilesContext CreateContext()
    {
        _ = Context.Database.EnsureCreated();
        Console.WriteLine(Context.Files.Count());
        AddMockFiles(Context);
        _ = Context.SaveChanges();
        return Context;
    }

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
                Context.Dispose();
            }

            disposedValue = true;
        }
    }

    private static void AddMockFiles(FilesContext mockFilesContext)
    {
        var filesAsJson = File.ReadAllText(@"TestFiles\files.json");

        var listFromJson = JsonSerializer.Deserialize<IEnumerable<FileDetail>>(filesAsJson)!;

        foreach(var item in listFromJson)
        {
            if(mockFilesContext.Files.FirstOrDefault(f => f.FileName == item.FileName && f.DirectoryName == item.DirectoryName) == null)
            {
                Console.WriteLine($"About to add {item.FullNameWithPath}");
                mockFilesContext.Files.Add(item);
                mockFilesContext.SaveChanges();
            }
        }

        JsonSerializer.Serialize(mockFilesContext.Files);
    }
}
