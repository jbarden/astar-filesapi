using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
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
        var specifiedFile = context.Files.FirstOrDefault(file => file.DirectoryName == directory && file.FileName == fileName);
        if(specifiedFile != null)
        {
            specifiedFile.SoftDeletePending = true;
            _ = context.SaveChanges();
        }

        logger.LogDebug("File {FileName} marked for deletion", request);
        await Task.Delay(1, cancellationToken);

        return NoContent();
    }
}
