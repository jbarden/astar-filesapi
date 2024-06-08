using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class MarkForHardDeletion(FilesContext context, ILogger<MarkForHardDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<int>
                    .WithActionResult
{
    [HttpPut("mark-for-hard-deletion")]
    [SwaggerOperation(
        Summary = "Mark the specified file for hard deletion",
        Description = "Mark the specified file for hard deletion - the file will NOT be deleted, just marked for hard deletion, please run the separate delete method to actually delete the file.",
        OperationId = "Files_MarkForHardDeletion",
        Tags = ["Files"])
]
    public override async Task<ActionResult> HandleAsync(int request, CancellationToken cancellationToken = default)
    {
        var specifiedFile = await context.FileAccessDetails.FirstOrDefaultAsync(file => file.Id == request, cancellationToken: cancellationToken);
        if(specifiedFile != null)
        {
            specifiedFile.HardDeletePending = true;
            _ = await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug("File {FileName} marked for deletion", request);

        return NoContent();
    }
}
