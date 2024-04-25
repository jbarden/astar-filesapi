using System.IO.Abstractions;
using AStar.FilesApi.Config;
using AStar.FilesApi.Models;
using AStar.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Controllers;

[Route("api/filesCount")]
[ApiController]
public class FilesListCountController(IFileSystem fileSystem, FilesContext context, ILogger<FilesControllerBase> logger) : FilesControllerBase(fileSystem, context, logger)
{
    [HttpGet(Name = "FilesListCount")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<int> Get([FromQuery] SearchParameters searchParameters)
    {
        var filesList = FileInfoFromContext(searchParameters).ToList();
        filesList = filesList.Where(f => f.FileSize > 0).ToList();

        if(searchParameters.SearchType is SearchType.Images)
        {
            filesList = filesList.Where(f => f.IsImage2).ToList();
        }

        Logger.LogInformation("Starting search for {SearchType}", searchParameters.SearchType);
        if(searchParameters.SearchType is not SearchType.Duplicates)
        {
            return Ok(filesList.Count);
        }

        Logger.LogInformation("Starting duplicate search");
        var duplicateFileInfoJbs = DuplicateFileInfoJbs(filesList.Select(f => new FileInfoDto { FullName = Path.Combine(f.DirectoryName, f.FileName), Size = f.FileSize, Name = f.FileName }));
        Logger.LogInformation("Found {DuplicateCount} for {SearchParameters}", duplicateFileInfoJbs.Count,
            searchParameters);

        return Ok(duplicateFileInfoJbs.Count);
    }
}
