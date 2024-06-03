using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class UndoMarkForHardDeletion(FilesContext context, ILogger<UndoMarkForHardDeletion> logger)
            : EndpointBaseAsync
                    .WithRequest<string>
                    .WithActionResult
{
    [HttpDelete("undo-mark-for-hard-deletion")]
    [SwaggerOperation(
        Summary = "Undo marking the specified file for hard deletion",
        Description = "Undo marking the specified file for hard deletion - the file will NOT be deleted, just UndoMarked for hard deletion, please run the separate delete method to actually delete the file.",
        OperationId = "Files_UndoMarkForHardDeletion",
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
            specifiedFile.HardDeletePending = false;
            _ = context.SaveChanges();
        }

        logger.LogDebug("File {FileName} UndoMarked for deletion", request);
        await Task.Delay(1, cancellationToken);

        return NoContent();
    }
}
