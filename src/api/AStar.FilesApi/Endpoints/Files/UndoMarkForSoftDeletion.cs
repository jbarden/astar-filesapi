using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class UndoMarkForSoftDeletion(FilesContext context, ILogger<MarkForSoftDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<int>
                    .WithActionResult
{
    [HttpPut("undo-mark-for-soft-deletion")]
    [SwaggerOperation(
        Summary = "Undo marking the specified file for soft deletion",
        Description = "Undo marking the specified file for soft deletion.",
        OperationId = "Files_UndoMarkForSoftDeletion",
        Tags = ["Files"])
]
    public override async Task<ActionResult> HandleAsync(int request, CancellationToken cancellationToken = default)
    {
        var specifiedFile = await context.FileAccessDetails.FirstOrDefaultAsync(file => file.Id == request, cancellationToken: cancellationToken);
        if(specifiedFile != null)
        {
            specifiedFile.SoftDeletePending = false;
            _ = await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug("File {FileName} mark for deletion has been undone", specifiedFile);

        return NoContent();
    }
}
