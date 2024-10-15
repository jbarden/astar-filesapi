using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class CountDuplicatesShould(CountDuplicatesFixture mockFilesFixture) : IClassFixture<CountDuplicatesFixture>
{
    [Fact]
    public async Task ReturnBadRequestWhenNoSearchFolderSpecified()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){ SearchFolder = string.Empty }, CancellationToken.None)).Result as BadRequestObjectResult;

        _ = response?.Value.Should().Be("A Search folder must be specified.");
    }

    [Fact]
    public async Task GetTheExpectedCountOfDuplicateFileGroupsWhenStartingAtTheRootFolder()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", Recursive = true }, CancellationToken.None)).Result as OkObjectResult;

        _ = response!.Value.Should().Be(20);
    }

    [Fact]
    public async Task GetTheExpectedCountOfDuplicateFileGroupsWhenStartingAtSubFolder()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\temp\AI", Recursive = true }, CancellationToken.None)).Result as OkObjectResult;

        _ = response!.Value.Should().Be(13);
    }
}
