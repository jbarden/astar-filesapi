using System.Reflection;
using System.Text;
using Ardalis.ApiEndpoints;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace AStar.FilesApi.Endpoints.Root;

[Route("/")]
public class Get(ILogger<Get> logger) : EndpointBaseAsync
                        .WithoutRequest
                        .WithActionResult<IEnumerable<LinkDto>>
{
    //[ResponseCache(Duration = 120)]
    [Produces("application/json")]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Root Document",
        Description = "List all of the available root methods",
        OperationId = "Root_List",
        Tags = ["Root"])
]
    public override async Task<ActionResult<IEnumerable<LinkDto>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var links = await GetLinks(cancellationToken);

        return Ok(links);
    }

    private async Task<List<LinkDto>> GetLinks(CancellationToken cancellationToken = default)
    {
        var links = new List<LinkDto>();
        await Task.Delay(1, cancellationToken);
        if(cancellationToken.IsCancellationRequested)
        {
            return links;
        }

        var endpoints = Assembly.GetExecutingAssembly().GetTypes().Where(t=> t.Namespace?.Contains("Endpoints")==true && t.BaseType?.Name.Contains("With") == true);

        foreach(var endpoint in endpoints)
        {
            if(endpoint != null)
            {
                var customAttributes = endpoint.GetCustomAttributes(typeof(RouteAttribute)).FirstOrDefault() as RouteAttribute;
                var routeBuilder = new StringBuilder();
                _ = routeBuilder.Append(customAttributes?.Template ?? "");
                var rel = endpoint.ReflectedType?.Name;
                var methods = endpoint.GetMethods().Where(f=>f.DeclaringType?.FullName?.Contains("AStar.FilesApi.Endpoints") == true);
                var httpMethod = "GET";
                if(methods.Any())
                {
                    foreach(var item in methods)
                    {
                        var httpMethodAttributes = item?.GetCustomAttributes(typeof(HttpMethodAttribute));
                        if(httpMethodAttributes is not null)
                        {
                            foreach(var httpMethodAttribute in httpMethodAttributes)
                            {
                                logger.LogInformation(((HttpMethodAttribute)httpMethodAttribute)?.Template);
                                var getAttribute = httpMethodAttribute as HttpGetAttribute;
                                if(getAttribute is not null && getAttribute.Template.IsNotNullOrWhiteSpace())
                                {
                                    _ = routeBuilder.Append($"/{getAttribute.Template}");
                                    httpMethod = "GET";
                                    rel = getAttribute.Template;
                                }

                                var postAttribute = httpMethodAttribute as HttpPostAttribute;
                                if(postAttribute is not null && postAttribute.Template.IsNotNullOrWhiteSpace())
                                {
                                    _ = routeBuilder.Append($"/{postAttribute.Template}");
                                    httpMethod = "POST";
                                }

                                var deleteAttribute = httpMethodAttribute as HttpDeleteAttribute;
                                if(deleteAttribute is not null && deleteAttribute.Template.IsNotNullOrWhiteSpace())
                                {
                                    _ = routeBuilder.Append($"/{deleteAttribute.Template}");
                                    httpMethod = "DELETE";
                                }

                                var putAttribute = httpMethodAttribute as HttpPutAttribute;
                                if(putAttribute is not null && putAttribute.Template.IsNotNullOrWhiteSpace())
                                {
                                    _ = routeBuilder.Append($"/{putAttribute.Template}");
                                    httpMethod = "PUT";
                                }

                                var patchAttribute = httpMethodAttribute as HttpPatchAttribute;
                                if(patchAttribute is not null && patchAttribute.Template.IsNotNullOrWhiteSpace())
                                {
                                    _ = routeBuilder.Append($"/{patchAttribute.Template}");
                                    httpMethod = "PATCH";
                                }
                            }
                        }
                    }
                }

                var route = routeBuilder.ToString();
                if(route.IsNotNullOrWhiteSpace())
                {
                    links.Add(new LinkDto() { Rel = rel ?? "self", Href = route, Method = httpMethod });
                }
            }
        }

        return links;
    }
}
