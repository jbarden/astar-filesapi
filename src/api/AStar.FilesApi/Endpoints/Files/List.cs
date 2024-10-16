﻿using System.Net.Mime;
using Ardalis.ApiEndpoints;
using AStar.FilesApi.Models;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static AStar.Infrastructure.EnumerableExtensions;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class List(FilesContext context, ILogger<List> logger)
            : EndpointBaseAsync
                    .WithRequest<ListSearchParameters>
                    .WithActionResult<IReadOnlyCollection<FileInfoDto>>
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
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
        }

        _ = await context.SaveChangesAsync(cancellationToken);

        return Ok(fileList);
    }
}
