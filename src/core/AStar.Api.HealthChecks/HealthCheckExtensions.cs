namespace AStar.Api.HealthChecks;

/// <summary>
/// The <see cref="HealthCheckExtensions"/> class contains the relevant method(s) to configure the endpoints.
/// </summary>
public static class HealthCheckExtensions
{
    /// <summary>
    /// The <see cref="ConfigureHealthCheckEndpoints"/> method will add a basic health/live and health/ready endpoint.
    /// </summary>
    /// <param name="app">An instance of <see cref="WebApplication"/> to configure.</param>
    /// <returns>The original <see cref="WebApplication"/> to facilitate further method chaining.</returns>
    public static WebApplication ConfigureHealthCheckEndpoints(this WebApplication app)
    {
        _ = app.MapHealthChecks("/health/live", new()
        {
            Predicate = _ => false,
            ResponseWriter = HealthCheckResponses.WriteJsonResponse
        });
        _ = app.MapHealthChecks("/health/ready", new()
        {
            ResponseWriter = HealthCheckResponses.WriteJsonResponse
        });

        return app;
    }
}
