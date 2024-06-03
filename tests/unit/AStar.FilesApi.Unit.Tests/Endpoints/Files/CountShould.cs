using AStar.FilesApi.Config;
using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class CountShould : IClassFixture<CountFixture>
{
    private readonly CountFixture mockFilesFixture;

    public CountShould(CountFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task ReturnBadRequestWhenNoSearchFolderSpecified()
    {
        var response = (await mockFilesFixture.SUT.HandleAsync(new(){ SearchFolder = string.Empty })).Result as BadRequestObjectResult;

        _ = response?.Value.Should().BeEquivalentTo(new { error = "A Search folder must be specified." });
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatCapturesAllFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 364;

        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", SearchType = SearchType.All})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatCapturesAllImageFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 265;

        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"c:\", SearchType = SearchType.Images})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsTopLevelFolderOnlyWhichIsEmpty()
    {
        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"d:\", Recursive = false})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(0);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursively()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 87;
        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"C:\Temp\Famous", Recursive = true})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatCapturesAllSupportedImageTypesFromStartingSubFolder()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 4;

        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"C:\Temp\Famous\coats", Recursive = false, SearchType = SearchType.Images})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public async Task ReturnBadRequestForDuplicates()
    {
        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"C:\", Recursive = true, SearchType = SearchType.Duplicates})).Result as BadRequestObjectResult;

        _ = response!.Value.Should().Be("Duplicate searches are not supported by this endpoint, please call the duplicate-specific endpoint.");
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeleted()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 95;

        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"C:\Temp\Famous", Recursive = true, IncludeSoftDeleted = true})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 87;

        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"C:\Temp\Famous", Recursive = true, IncludeMarkedForDeletion = true})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public async Task GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeletedAndIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 95;

        var response = (await  mockFilesFixture.SUT.HandleAsync(new(){SearchFolder = @"C:\Temp\Famous", Recursive = true, IncludeSoftDeleted = true, IncludeMarkedForDeletion = true})).Result as OkObjectResult;

        _ = response!.Value.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }
}
