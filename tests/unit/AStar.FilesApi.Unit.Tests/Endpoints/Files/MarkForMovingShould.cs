using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class MarkForMovingShould : IClassFixture<MarkForMovingFixture>
{
    private readonly MarkForMovingFixture mockFilesFixture;

    public MarkForMovingShould(MarkForMovingFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task GetTheExpectedCountWhenMarkFileForMovingWasSuccessful()
    {
        var testFile = mockFilesFixture.MockFilesContext.FileAccessDetails.First(file=>!file.MoveRequired);

        _ = await mockFilesFixture.SUT.HandleAsync(new() { Id = testFile.Id }) as OkObjectResult;

        mockFilesFixture.MockFilesContext.FileAccessDetails.Count(file => file.Id == testFile.Id && file.MoveRequired).Should().Be(1);
    }
}
