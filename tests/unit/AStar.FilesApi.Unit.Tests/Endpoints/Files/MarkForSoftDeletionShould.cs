using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class MarkForSoftDeletionShould : IClassFixture<MarkForSoftDeletionFixture>
{
    private readonly MarkForSoftDeletionFixture mockFilesFixture;

    public MarkForSoftDeletionShould(MarkForSoftDeletionFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task GetTheExpectedCountWhenMarkFileForDeletionWasSuccessful()
    {
        var testFile = mockFilesFixture.MockFilesContext.FileAccessDetails.First(file=>!file.SoftDeletePending);

        _ = await mockFilesFixture.SUT.HandleAsync(testFile.Id) as OkObjectResult;

        mockFilesFixture.MockFilesContext.FileAccessDetails.Count(file => file.Id == testFile.Id && file.SoftDeletePending).Should().Be(1);
    }
}
