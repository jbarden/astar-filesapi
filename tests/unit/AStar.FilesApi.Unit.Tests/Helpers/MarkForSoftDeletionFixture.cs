using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class MarkForSoftDeletionFixture : IDisposable
{
    private bool disposedValue;

    public MarkForSoftDeletionFixture() => SUT = new MarkForSoftDeletion(MockFilesContext, NullLogger<MarkForSoftDeletion>.Instance);

    public FilesContext MockFilesContext => Helpers.MockFilesContext.CreateContext();

    public MarkForSoftDeletion SUT { get; }

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
