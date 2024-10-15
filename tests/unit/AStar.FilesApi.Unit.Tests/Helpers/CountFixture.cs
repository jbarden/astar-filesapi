using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class CountFixture : IDisposable
{
    private bool disposedValue;

    public CountFixture() => SUT = new Count(MockFilesContext, NullLogger<Count>.Instance);

    public FilesContext MockFilesContext => Helpers.MockFilesContext.CreateContext();

    public Count SUT { get; }

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
