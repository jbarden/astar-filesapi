using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class CountDuplicatesFixture : IDisposable
{
    private bool disposedValue;

    public CountDuplicatesFixture() => SUT = new CountDuplicates(MockFilesContext, NullLogger<CountDuplicates>.Instance);

    public FilesContext MockFilesContext => Helpers.MockFilesContext.CreateContext();

    public CountDuplicates SUT { get; }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(!disposedValue)
        {
            if(disposing)
            {
                MockFilesContext.Dispose();
            }

            disposedValue = true;
        }
    }
}
