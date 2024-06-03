using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class UndoMarkForHardDeletionFixture : IDisposable
{
    private bool disposedValue;

    public UndoMarkForHardDeletionFixture()
    {
        MockFilesContext = new MockFilesContext().CreateContext();
        SUT = new UndoMarkForHardDeletion(MockFilesContext, NullLogger<UndoMarkForHardDeletion>.Instance);
    }

    public FilesContext MockFilesContext { get; }

    public UndoMarkForHardDeletion SUT { get; }

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
