using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class CountFixture : IDisposable
{
    private bool disposedValue;

    public CountFixture()
    {
        MockFilesContext = new MockFilesContext().CreateContext();
        SUT = new Count(MockFilesContext, NullLogger<Count>.Instance);
    }

    public FilesContext MockFilesContext { get; }

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
