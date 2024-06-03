using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class MarkForHardDeletion(FilesContext context, ILogger<MarkForHardDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<string>
                    .WithActionResult
{
    [HttpDelete("mark-for-hard-deletion")]
    [SwaggerOperation(
        Summary = "Mark the specified file for hard deletion",
        Description = "Mark the specified file for hard deletion - the file will NOT be deleted, just marked for hard deletion, please run the separate delete method to actually delete the file.",
        OperationId = "Files_MarkForHardDeletion",
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
            specifiedFile.HardDeletePending = true;
            _ = context.SaveChanges();
        }

        logger.LogDebug("File {FileName} marked for deletion", request);
        await Task.Delay(1, cancellationToken);

        return NoContent();
    }
}
