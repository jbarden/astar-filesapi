using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class UndoMarkForSoftDeletion(FilesContext context, ILogger<MarkForSoftDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<string>
                    .WithActionResult
{
    [HttpDelete("undo-mark-for-soft-deletion")]
    [SwaggerOperation(
        Summary = "Undo marking the specified file for soft deletion",
        Description = "Undo marking the specified file for soft deletion.",
        OperationId = "Files_UndoMarkForSoftDeletion",
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
        var specifiedFile = await context.Files.FirstOrDefaultAsync(file => file.DirectoryName == directory && file.FileName == fileName, cancellationToken: cancellationToken);
        if(specifiedFile != null)
        {
            var fileDetail = await context.FileAccessDetails.FirstAsync(fileDetail => fileDetail.Id == specifiedFile.Id, cancellationToken: cancellationToken);
            fileDetail.SoftDeletePending = false;
            _ = await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug("File {FileName} mark for deletion has been undone", specifiedFile);
        await Task.Delay(1, cancellationToken);

        return NoContent();
    }
}
