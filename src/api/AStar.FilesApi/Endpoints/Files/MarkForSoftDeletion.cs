using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class MarkForSoftDeletion(FilesContext context, ILogger<MarkForSoftDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<string>
                    .WithActionResult
{
    [HttpDelete("mark-for-soft-deletion")]
    [SwaggerOperation(
        Summary = "Mark the specified file for soft deletion",
        Description = "Mark the specified file for soft deletion - the file will NOT be deleted, just marked for soft deletion, please run the separate delete method to actually delete the file.",
        OperationId = "Files_MarkForSoftDeletion",
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
            fileDetail.SoftDeletePending = true;
            _ = await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug("File {FileName} marked for deletion", request);
        await Task.Delay(1, cancellationToken);

        return NoContent();
    }
}
