using AStar.FilesApi.Config;
using AStar.FilesApi.Files;

namespace AStar.FilesApi.Endpoints.Files;

public class ListSearchParametersShould
{
    [Fact]
    public void GenerateTheExpectedToStringOutput()
    {
        var sut = new ListSearchParameters().ToString();

        sut.Should().Be(@"{""SearchFolder"":"""",""Recursive"":true,""ExcludeViewed"":false,""IncludeSoftDeleted"":false,""IncludeMarkedForDeletion"":false,""SearchText"":null,""CurrentPage"":1,""ItemsPerPage"":10,""MaximumSizeOfThumbnail"":150,""MaximumSizeOfImage"":1500,""SortOrder"":""SizeDescending"",""SearchType"":""Images""}");
    }

    [Fact]
    public void ContainTheSearchTypeSetAsImagesWhenNotSpecified() => new ListSearchParameters().SearchType.Should().Be(SearchType.Images);

    [Fact]
    public void ContainTheSearchTypeSetAsSpecified() => new ListSearchParameters() { SearchType = SearchType.All }.SearchType.Should().Be(SearchType.All);
}
