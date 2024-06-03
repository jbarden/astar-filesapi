using System.Text;
using Ardalis.ApiEndpoints;
using AStar.FilesApi.Files;
using AStar.FilesApi.Models;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using static AStar.Infrastructure.EnumerableExtensions;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class List(FilesContext context, ILogger<List> logger)
            : EndpointBaseAsync
                    .WithRequest<ListSearchParameters>
                    .WithActionResult<IReadOnlyCollection<FileInfoDto>>
{
    [ResponseCache(Duration = 120)]
    [HttpGet("list")]
    [SwaggerOperation(
        Summary = "List the matching files",
        Description = "List the files matching the criteria",
        OperationId = "Files_List",
        Tags = ["Files"])
]
    public override async Task<ActionResult<IReadOnlyCollection<FileInfoDto>>> HandleAsync([FromQuery] ListSearchParameters request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        if(request.SearchFolder.IsNullOrWhiteSpace())
        {
            return BadRequest("A Search folder must be specified.");
        }

        logger.LogDebug("Starting {SearchType} search...{FullParameters}", request.SearchType, request);

        var files = context.Files
                           .GetMatchingFiles(request.SearchFolder, request.Recursive, request.SearchType.ToString(), request.IncludeSoftDeleted, request.IncludeMarkedForDeletion, request.ExcludeViewed, cancellationToken)
                           .OrderFiles(request.SortOrder);

        var fileList = new List<FileInfoDto>();

        foreach(var file in files.Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage))
        {
            fileList.Add(new FileInfoDto(file));
            file.LastViewed = DateTime.UtcNow;
        }

        _ = context.SaveChanges();
        await Task.Delay(1, cancellationToken);

        return Ok(fileList);
    }
}
