using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class MarkForMovingFixture : IDisposable
{
    private bool disposedValue;

    public MarkForMovingFixture()
    {
        MockFilesContext = new MockFilesContext().CreateContext();
        SUT = new MarkForMoving(MockFilesContext, NullLogger<MarkForMoving>.Instance);
    }

    public FilesContext MockFilesContext { get; }

    public MarkForMoving SUT { get; }

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
