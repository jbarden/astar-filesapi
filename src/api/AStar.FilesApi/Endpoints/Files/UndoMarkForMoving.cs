using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class UndoMarkForMoving(FilesContext context, ILogger<UndoMarkForMoving> logger)
            : EndpointBaseAsync
                    .WithRequest<int>
                    .WithActionResult
{
    [HttpPut("undo-mark-for-moving")]
    [SwaggerOperation(
        Summary = "Undo marking the specified file for moving later",
        Description = "Undo marking the specified file for moving - the file will NOT be moved, just marked for moving. Please use the applicable page in the portal to actually perform the move.",
        OperationId = "Files_UndoMarkForMoving",
        Tags = ["Files"])
]
    public override async Task<ActionResult> HandleAsync(int request, CancellationToken cancellationToken = default)
    {
        var specifiedFile = await context.FileAccessDetails.FirstOrDefaultAsync(file => file.Id == request, cancellationToken: cancellationToken);
        if(specifiedFile != null)
        {
            specifiedFile.MoveRequired = false;
            _ = await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug("File {FileName} marked for deletion", request);

        return NoContent();
    }
}
