using Ardalis.ApiEndpoints;
using AStar.Infrastructure.Data;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Files;

[Route("api/files")]
public class Update(FilesContext context, ILogger<Update> logger) : EndpointBaseAsync
                            .WithRequest<DirectoryChangeRequest>
                            .WithActionResult
{
    [HttpPut("update-directory")]
    [SwaggerOperation(
        Summary = "Updates the file directory",
        Description = "Updates the file directory from the specified old directory to the specified new directory.",
        OperationId = "Files_UpdateDirectory",
        Tags = ["Files"])
]
    public override async Task<ActionResult> HandleAsync(DirectoryChangeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var specifiedFile = await context.Files.FirstOrDefaultAsync(file => file.DirectoryName == request.OldDirectoryName && file.FileName == request.FileName, cancellationToken: cancellationToken);
        var error = string.Empty;

        if(specifiedFile != null)
        {
            var newLocation = specifiedFile.DirectoryName.Replace(request.OldDirectoryName, request.NewDirectoryName);

            try
            {
                var newNameAndLocation = Path.Combine(newLocation, request.FileName);
                if(System.IO.File.Exists(specifiedFile.FullNameWithPath))
                {
                    System.IO.File.Move(specifiedFile.FullNameWithPath, newNameAndLocation, true);
                }
            }
            catch(DirectoryNotFoundException ex)
            {
                logger.LogError(ex, "The specified directory could not be found. File: {FileName} in Directory: {OldDirectoryName} or target directory: {NewDirectoryName}", request.FileName, request.NewDirectoryName, request.OldDirectoryName);
                error = $"The specified directory could not be found. File: {request.FileName} in Directory: {request.OldDirectoryName} or target directory: {request.NewDirectoryName}";
            }
            catch(FileNotFoundException ex)
            {
                logger.LogError(ex, "The specified file could not be found. File: {FileName} in Directory: {OldDirectoryName} or target directory: {NewDirectoryName}", request.FileName, request.NewDirectoryName, request.OldDirectoryName);
                error = $"The specified file could not be found. File: {request.FileName} in Directory: {request.OldDirectoryName}";
            }

            var targetFile = await context.Files.FirstOrDefaultAsync(file => file.DirectoryName == newLocation && file.FileName == request.FileName, cancellationToken: cancellationToken);

            if(targetFile != null)
            {
                _ = context.Files.Remove(targetFile);
            }

            _ = context.Files.Remove(specifiedFile);
            _ = await context.SaveChangesAsync(cancellationToken);
            var fileDetail = await context.FileAccessDetails.FirstAsync(fileDetail => fileDetail.Id == specifiedFile.Id, cancellationToken: cancellationToken);
            fileDetail.SoftDeletePending = false;
            fileDetail.DetailsLastUpdated = DateTime.UtcNow;
            specifiedFile.DirectoryName = request.NewDirectoryName;
            _ = await context.Files.AddAsync(specifiedFile, cancellationToken: cancellationToken);
            _ = await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug("File {FileName} moved to the new {NewDirectory} from {OldDirectory}", request.FileName, request.NewDirectoryName, request.OldDirectoryName);
        await Task.Delay(1, cancellationToken);

        return error.IsNotNullOrWhiteSpace()
                        ? BadRequest(error)
                        : NoContent();
    }
}
