using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class MarkForSoftDeletion(FilesContext context, ILogger<MarkForSoftDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<Request>
                    .WithActionResult
{
    [HttpPut("mark-for-soft-deletion")]
    [SwaggerOperation(
        Summary = "Mark the specified file for soft deletion",
        Description = "Mark the specified file for soft deletion - the file will NOT be deleted, just marked for soft deletion, please run the separate delete method to actually delete the file.",
        OperationId = "Files_MarkForSoftDeletion",
        Tags = ["Files"])
]
    public override async Task<ActionResult> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        var specifiedFile = await context.FileAccessDetails.FirstOrDefaultAsync(file => file.Id == request.Id, cancellationToken: cancellationToken);
        if(specifiedFile != null)
        {
            specifiedFile.SoftDeletePending = true;
            _ = await context.SaveChangesAsync(cancellationToken);
            logger.LogDebug("File {FileName} marked for soft deletion", request);

            return NoContent();
        }

        logger.LogDebug("File {FileName} could not be found - mark for soft deletion cannot be performed", request);

        return NotFound();
    }
}
