using AStar.FilesApi.Files;
using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class ListDuplicatesShould : IClassFixture<ListDuplicatesFixture>
{
    private readonly ListDuplicatesFixture mockFilesFixture;

    public ListDuplicatesShould(ListDuplicatesFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task ReturnBadRequestWhenNoSearchFolderSpecified()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){ SearchFolder = string.Empty })).Result as BadRequestObjectResult;

        _ = response?.Value.Should().Be("A Search folder must be specified.");
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsTopLevelFolderOnlyWhichIsEmpty()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", Recursive=false})).Result as OkObjectResult;

        var value = (IReadOnlyCollection<DuplicateGroup>)response!.Value!;

        _ = value.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetTheExpectedSecondPageOfFilesWhenTheFilterApplied()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", ItemsPerPage = 20, CurrentPage = 2})).Result as OkObjectResult;

        var value = (IReadOnlyCollection<DuplicateGroup>)response!.Value!;

        value.Count.Should().Be(16);
        await Verify(value);
    }
}
