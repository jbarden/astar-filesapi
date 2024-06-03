using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.FilesApi.Endpoints.Root;

public class GetShould
{
    [Fact]
    public async Task ReturnTheExpectedRootDocument()
    {
        var sut = new Get(NullLogger<Get>.Instance);

        var response = (await sut.HandleAsync()).Result as OkObjectResult;

        await Verify(response?.Value);
    }
}
