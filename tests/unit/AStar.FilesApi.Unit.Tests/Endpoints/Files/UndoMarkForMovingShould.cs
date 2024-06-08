using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class UndoMarkForMovingShould : IClassFixture<UndoMarkForMovingFixture>
{
    private readonly UndoMarkForMovingFixture mockFilesFixture;

    public UndoMarkForMovingShould(UndoMarkForMovingFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task GetTheExpectedCountWhenMarkFileForMovingWasSuccessful()
    {
        var testFile = mockFilesFixture.MockFilesContext.FileAccessDetails.First();
        testFile.MoveRequired = true;
        _ = await mockFilesFixture.MockFilesContext.SaveChangesAsync();

        _ = await mockFilesFixture.SUT.HandleAsync(testFile.Id) as OkObjectResult;

        mockFilesFixture.MockFilesContext.FileAccessDetails.Count(file => file.Id == testFile.Id && file.MoveRequired).Should().Be(0);
    }
}
