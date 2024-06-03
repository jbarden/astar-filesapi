using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class UndoMarkForMovingShould : IClassFixture<UndoMarkForMovingFixture>
{
    private readonly UndoMarkForMovingFixture mockFilesFixture;

    public UndoMarkForMovingShould(UndoMarkForMovingFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData("Ssdfsdfsdfsdfdsarch")]
    public async Task ReturnBadRequestWhenNoValidPathSpecified(string fileWithPath)
    {
        var response = await mockFilesFixture.SUT.HandleAsync(fileWithPath) as BadRequestObjectResult;

        _ = response?.Value.Should().Be("A valid file with path must be specified.");
    }

    [Fact]
    public async Task GetTheExpectedCountWhenMarkFileForMovingWasSuccessful()
    {
        var testFile = mockFilesFixture.MockFilesContext.Files.First();
        testFile.NeedsToMove = true;
        mockFilesFixture.MockFilesContext.SaveChanges();

        _ = await mockFilesFixture.SUT.HandleAsync(Path.Combine(testFile.DirectoryName, testFile.FileName)) as OkObjectResult;

        mockFilesFixture.MockFilesContext.Files.Count(file => file.DirectoryName == testFile.DirectoryName && file.FileName == testFile.FileName && file.NeedsToMove).Should().Be(0);
    }
}
