using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class UndoMarkForSoftDeletionShould : IClassFixture<UndoMarkForSoftDeletionFixture>
{
    private readonly UndoMarkForSoftDeletionFixture mockFilesFixture;

    public UndoMarkForSoftDeletionShould(UndoMarkForSoftDeletionFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task GetTheExpectedCountWhenUndoMarkFileForDeletionWasSuccessful()
    {
        var testFile = mockFilesFixture.MockFilesContext.FileAccessDetails.First(file=>file.SoftDeletePending);

        _ = await mockFilesFixture.SUT.HandleAsync(testFile.Id) as OkObjectResult;

        mockFilesFixture.MockFilesContext.FileAccessDetails.Count(file => file.Id == testFile.Id && file.SoftDeletePending).Should().Be(0);
    }
}
