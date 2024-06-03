using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AStar.Api.HealthChecks;

/// <summary>
/// The <see cref="HealthCheckResponses"/> class contains the available extension methods for writing Health Checks.
/// </summary>
public static class HealthCheckResponses
{
    /// <summary>
    /// The <see cref="WriteJsonResponse"/> method will return the applicable HealthCheck data.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="report"></param>
    /// <returns>The applicable HealthCheck data in JSON format.</returns>
    public static async Task WriteJsonResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json; charset=utf-8";

        var options = new JsonWriterOptions { Indented = true };

        await using var writer = new Utf8JsonWriter(context.Response.Body, options);

        writer.WriteStartObject();
        writer.WriteString("status", report.Status.ToString());

        if(report.Entries.Count > 0)
        {
            writer.WriteStartArray("results");

            foreach(var (key, value) in report.Entries)
            {
                writer.WriteStartObject();
                writer.WriteString("key", key);
                writer.WriteString("status", value.Status.ToString());
                writer.WriteString("description", value.Description);
                writer.WriteStartArray("data");
                foreach(var (dataKey, dataValue) in value.Data)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName(dataKey);
                    JsonSerializer.Serialize(writer, dataValue, dataValue.GetType());
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        writer.WriteEndObject();
    }
}
