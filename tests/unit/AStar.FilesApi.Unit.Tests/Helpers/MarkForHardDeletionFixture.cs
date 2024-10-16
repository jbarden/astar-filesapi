﻿using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class MarkForHardDeletionFixture : IDisposable
{
    private bool disposedValue;

    public MarkForHardDeletionFixture() => SUT = new MarkForHardDeletion(MockFilesContext, NullLogger<MarkForHardDeletion>.Instance);

    public FilesContext MockFilesContext => Helpers.MockFilesContext.CreateContext();

    public MarkForHardDeletion SUT { get; }

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
