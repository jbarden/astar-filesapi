using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class UndoMarkForMovingFixture : IDisposable
{
    private bool disposedValue;

    public UndoMarkForMovingFixture()
    {
        MockFilesContext = new MockFilesContext().CreateContext();
        SUT = new UndoMarkForMoving(MockFilesContext, NullLogger<UndoMarkForMoving>.Instance);
    }

    public FilesContext MockFilesContext { get; }

    public UndoMarkForMoving SUT { get; }

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
