using AStar.FilesApi.Config;
using AStar.FilesApi.Helpers;
using AStar.FilesApi.Models;
using AStar.Infrastructure.Models;

using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class ListShould : IClassFixture<ListFixture>
{
    private const int ExpectedResultCountWithDefaultSearchParameters = 10;
    private readonly ListFixture mockFilesFixture;

    public ListShould(ListFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task ReturnBadRequestWhenNoSearchFolderSpecified()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){ SearchFolder = string.Empty }, CancellationToken.None)).Result as BadRequestObjectResult;

        _ = response?.Value.Should().Be("A Search folder must be specified.");
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsTopLevelFolderOnlyWhichIsEmpty()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", Recursive=false}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        _ = value.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetTheExpectedListOfFilesWhenTheFilterAppliedCapturesAllFiles()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", SearchType = SearchType.All}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        _ = value.Count.Should().Be(ExpectedResultCountWithDefaultSearchParameters);
    }

    [Fact]
    public async Task GetTheFullListOfFilesWhenTheFilterAppliedCapturesAllFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 248;
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", SearchType = SearchType.All, ItemsPerPage = 10_000}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        _ = value.Count.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public async Task GetTheFullListContainingTheExpectedFilesWhenTheFilterAppliedCapturesAllFiles()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", SearchType = SearchType.All, ItemsPerPage = 10_000}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        await Verify(value);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatCapturesAllImageFiles()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", SearchType = SearchType.Images}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        _ = value.Count.Should().Be(ExpectedResultCountWithDefaultSearchParameters);
    }

    [Fact]
    public async Task GetTheExpectedFilesWhenFilterAppliedThatCapturesAllImageFiles()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", SearchType = SearchType.Images}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        await Verify(value);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursively()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\temp\Famous", Recursive = true}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        _ = value.Count.Should().Be(ExpectedResultCountWithDefaultSearchParameters);
    }

    [Fact]
    public async Task GetTheExpectedFilesWhenFilterAppliedThatTargetsSpecificFolderRecursively()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\temp\Famous", Recursive = true}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        await Verify(value);
    }

    [Fact]
    public async Task GetTheExpectedFilesWhenFilterAppliedThatCapturesAllSupportedImageTypesFromStartingSubFolderAnHonourTheSizeDescendingSortOrder()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\temp\", Recursive = true, SearchType = SearchType.Images, SortOrder = SortOrder.SizeDescending}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        await Verify(value);
    }

    [Fact]
    public async Task GetTheExpectedFilesWhenFilterAppliedThatCapturesAllSupportedImageTypesFromStartingSubFolderAnHonourTheSizeAscendingSortOrder()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\temp\", Recursive = true, SearchType = SearchType.Images, SortOrder = SortOrder.SizeAscending}, CancellationToken.None)).Result as OkObjectResult;

        var value = (IReadOnlyCollection<FileInfoDto>)response!.Value!;

        await Verify(value);
    }
}
