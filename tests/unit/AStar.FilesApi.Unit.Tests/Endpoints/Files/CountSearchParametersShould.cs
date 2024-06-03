using AStar.FilesApi.Config;
using AStar.FilesApi.Files;

namespace AStar.FilesApi.Endpoints.Files;

public class CountSearchParametersShould
{
    [Fact]
    public void GenerateTheExpectedToStringOutput()
    {
        var sut = new CountSearchParameters().ToString();

        sut.Should().Be(@"{""SearchFolder"":"""",""Recursive"":true,""ExcludeViewed"":false,""IncludeSoftDeleted"":false,""IncludeMarkedForDeletion"":false,""SearchText"":null,""SearchType"":""Images""}");
    }

    [Fact]
    public void ContainTheSearchTypeSetAsImagesWhenNotSpecified() => new CountSearchParameters().SearchType.Should().Be(SearchType.Images);

    [Fact]
    public void ContainTheSearchTypeSetAsSpecified() => new CountSearchParameters() { SearchType = SearchType.All }.SearchType.Should().Be(SearchType.All);
}
