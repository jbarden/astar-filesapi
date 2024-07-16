using AStar.FilesApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AStar.FilesApi.Endpoints.Files;

public class MarkForHardDeletionShould : IClassFixture<MarkForHardDeletionFixture>
{
    private readonly MarkForHardDeletionFixture mockFilesFixture;

    public MarkForHardDeletionShould(MarkForHardDeletionFixture mockFilesFixture) => this.mockFilesFixture = mockFilesFixture;

    [Fact]
    public async Task GetTheExpectedCountWhenMarkFileForHardDeletionWasSuccessful()
    {
        var testFile = mockFilesFixture.MockFilesContext.FileAccessDetails.First(file=>!file.HardDeletePending);

        _ = await mockFilesFixture.SUT.HandleAsync(new() { Id = testFile.Id }) as OkObjectResult;

        mockFilesFixture.MockFilesContext.FileAccessDetails.Count(file => file.Id == testFile.Id && file.HardDeletePending).Should().Be(1);
    }
}
