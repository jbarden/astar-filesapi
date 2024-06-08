using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class MarkForSoftDeletion(FilesContext context, ILogger<MarkForSoftDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<int>
                    .WithActionResult
{
    [HttpPut("mark-for-soft-deletion")]
    [SwaggerOperation(
        Summary = "Mark the specified file for soft deletion",
        Description = "Mark the specified file for soft deletion - the file will NOT be deleted, just marked for soft deletion, please run the separate delete method to actually delete the file.",
        OperationId = "Files_MarkForSoftDeletion",
        Tags = ["Files"])
]
    public override async Task<ActionResult> HandleAsync(int request, CancellationToken cancellationToken = default)
    {
        var specifiedFile = await context.FileAccessDetails.FirstOrDefaultAsync(file => file.Id == request, cancellationToken: cancellationToken);
        if(specifiedFile != null)
        {
            specifiedFile.SoftDeletePending = true;
            _ = await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug("File {FileName} marked for deletion", request);

        return NoContent();
    }
}
