using AStar.FilesApi.Files;

namespace AStar.FilesApi.Endpoints.Files;

public class CountDuplicatesSearchParametersShould
{
    [Fact]
    public void GenerateTheExpectedToStringOutput()
    {
        var sut = new CountDuplicatesSearchParameters().ToString();

        sut.Should().Be(@"{""SearchFolder"":"""",""Recursive"":true,""ExcludeViewed"":false,""IncludeSoftDeleted"":false,""IncludeMarkedForDeletion"":false,""SearchText"":null}");
    }
}
