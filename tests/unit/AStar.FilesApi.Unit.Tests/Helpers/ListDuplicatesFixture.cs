﻿using AStar.FilesApi.Endpoints.Files;
using AStar.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Helpers;

public class ListDuplicatesFixture : IDisposable
{
    private bool disposedValue;

    public ListDuplicatesFixture() => SUT = new ListDuplicates(MockFilesContext, NullLogger<ListDuplicates>.Instance);

    public FilesContext MockFilesContext => Helpers.MockFilesContext.CreateContext();

    public ListDuplicates SUT { get; }

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
