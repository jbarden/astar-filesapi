using AStar.FilesApi.Config;
using AStar.FilesApi.Files;

namespace AStar.FilesApi.Endpoints.Files;

public class ListDuplicatesSearchParametersShould
{
    [Fact]
    public void GenerateTheExpectedToStringOutput()
    {
        var sut = new ListDuplicatesSearchParameters().ToString();

        sut.Should().Be(@"{""SearchFolder"":"""",""Recursive"":true,""ExcludeViewed"":false,""IncludeSoftDeleted"":false,""IncludeMarkedForDeletion"":false,""SearchText"":null,""CurrentPage"":1,""ItemsPerPage"":10,""MaximumSizeOfThumbnail"":150,""MaximumSizeOfImage"":1500,""SortOrder"":""SizeDescending"",""SearchType"":""Duplicates""}");
    }

    [Fact]
    public void ContainTheSearchTypeSetAsDuplicates() => new ListDuplicatesSearchParameters().SearchType.Should().Be(SearchType.Duplicates);
}
