using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class UndoMarkForHardDeletionShould : IClassFixture<UndoMarkForHardDeletionFixture>
{
    private readonly UndoMarkForHardDeletionFixture mockFilesFixture;

    public UndoMarkForHardDeletionShould(UndoMarkForHardDeletionFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task GetTheExpectedCountWhenUndoMarkFileForDeletionWasSuccessful()
    {
        var testFile = mockFilesFixture.MockFilesContext.FileAccessDetails.First(file=>file.HardDeletePending);

        _ = await mockFilesFixture.SUT.HandleAsync(new() { Id = testFile.Id }) as OkObjectResult;

        mockFilesFixture.MockFilesContext.FileAccessDetails.Count(file => file.Id == testFile.Id && file.HardDeletePending).Should().Be(0);
    }
}
