using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class UndoMarkForSoftDeletionFixture : IDisposable
{
    private bool disposedValue;

    public UndoMarkForSoftDeletionFixture()
    {
        MockFilesContext = new MockFilesContext().CreateContext();
        SUT = new UndoMarkForSoftDeletion(MockFilesContext, NullLogger<MarkForSoftDeletion>.Instance);
    }

    public FilesContext MockFilesContext { get; }

    public UndoMarkForSoftDeletion SUT { get; }

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
