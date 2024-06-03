using Ardalis.ApiEndpoints;
using AStar.FilesApi.Config;
using AStar.FilesApi.Files;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class Count(FilesContext context, ILogger<Count> logger)
            : EndpointBaseAsync
                    .WithRequest<CountSearchParameters>
                    .WithActionResult<int>
{
    [HttpGet("count")]
    [Produces("application/vnd.astar.file-count+json", "application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get the count of files",
        Description = "Get the count of files matching the criteria",
        OperationId = "Files_Count",
        Tags = ["Files"])
]
    public override async Task<ActionResult<int>> HandleAsync([FromQuery] CountSearchParameters request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        if(request.SearchFolder.IsNullOrWhiteSpace())
        {
            return BadRequest(new { error = "A Search folder must be specified." });
        }

        if(request.SearchType == SearchType.Duplicates)
        {
            return BadRequest("Duplicate searches are not supported by this endpoint, please call the duplicate-specific endpoint.");
        }

        var matchingFilesCount = context.Files
                                        .GetMatchingFiles(request.SearchFolder, request.Recursive, request.SearchType.ToString(), request.IncludeSoftDeleted, request.IncludeMarkedForDeletion, request.ExcludeViewed, cancellationToken)
                                        .Count();

        logger.LogDebug("File Count: {FileCount}", matchingFilesCount);
        await Task.Delay(1, cancellationToken);

        return Ok(matchingFilesCount);
    }
}
