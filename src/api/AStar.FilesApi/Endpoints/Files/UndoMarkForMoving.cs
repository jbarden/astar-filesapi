using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class UndoMarkForMoving(FilesContext context, ILogger<UndoMarkForMoving> logger)
            : EndpointBaseAsync
                    .WithRequest<string>
                    .WithActionResult
{
    [HttpDelete("undo-mark-for-moving")]
    [SwaggerOperation(
        Summary = "Undo marking the specified file for moving later",
        Description = "Undo marking the specified file for moving - the file will NOT be moved, just marked for moving. Please use the applicable page in the portal to actually perform the move.",
        OperationId = "Files_UndoMarkForMoving",
        Tags = ["Files"])
]
    public override async Task<ActionResult> HandleAsync(string request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        if(request.IsNullOrWhiteSpace() || !request.Contains('\\'))
        {
            return BadRequest("A valid file with path must be specified.");
        }

        var index = request.LastIndexOf('\\');
        var directory = request[..index];
        var fileName = request[++index..];
        var specifiedFile = context.Files.FirstOrDefault(file => file.DirectoryName == directory && file.FileName == fileName);
        if(specifiedFile != null)
        {
            specifiedFile.NeedsToMove = false;
            _ = context.SaveChanges();
        }

        logger.LogDebug("File {FileName} marked for deletion", request);
        await Task.Delay(1, cancellationToken);

        return NoContent();
    }
}
