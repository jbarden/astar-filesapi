using System.IO.Abstractions;
using AStar.FilesApi.Config;
using AStar.FilesApi.Models;

using AStar.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Controllers;

[Route("api/files")]
[ApiController]
public class FilesListController(IFileSystem fileSystem, FilesContext context, ILogger<FilesControllerBase> logger)
    : FilesControllerBase(fileSystem, context, logger)
{
    [HttpGet(Name = "FilesList")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IAsyncEnumerable<FileInfoDto>> Get([FromQuery] SearchParameters searchParameters)
    {
        Logger.LogInformation("Starting search for {SearchType}", searchParameters.SearchType);
        var filesList = FileInfoFromContext(searchParameters).ToList();
        if(searchParameters.SearchType is SearchType.Images)
        {
            filesList = filesList.Where(f => f.IsImage2).ToList();
        }

        var fileInfos = filesList.Select(f => new FileInfoDto { FullName = Path.Combine(f.DirectoryName, f.FileName), Size = f.FileSize, Name = f.FileName, Height = f.Height, Width = f.Width });
        var skip = searchParameters.ItemsPerPage * (searchParameters.CurrentPage - 1);
        if(searchParameters.SearchType is SearchType.Duplicates)
        {
            Logger.LogInformation("Starting duplicate search");
            fileInfos = DuplicateFileInfoJbs(fileInfos);
            Logger.LogInformation("Found {DuplicateCount} duplicates for {SearchParameters}", fileInfos.Count(), searchParameters);
        }

        Logger.LogInformation("Setting Sort Order");
        fileInfos = searchParameters.SortOrder switch
        {
            SortOrder.NameAscending => fileInfos.OrderBy(file => file.FullName),
            SortOrder.SizeDescending => fileInfos.OrderByDescending(file => file.Size),
            SortOrder.SizeAscending => fileInfos.OrderBy(file => file.Size),
            SortOrder.NameDescending => fileInfos.OrderByDescending(file => file.FullName),
            _ => throw new ArgumentOutOfRangeException(nameof(searchParameters)),
        };

        fileInfos = fileInfos.Skip(skip).Take(searchParameters.ItemsPerPage).ToList();

        return Ok(fileInfos);
    }
}
