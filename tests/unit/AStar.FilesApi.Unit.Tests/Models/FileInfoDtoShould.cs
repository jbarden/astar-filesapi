using AStar.FilesApi.Models;

namespace AStar.FilesApi.Models;

public class FileInfoDtoShould
{
    [Fact]
    public Task ContainTheExpectedProperties()
    {
        var sut = new FileInfoDto();

        return Verify(sut);
    }
}
