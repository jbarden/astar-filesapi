using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace AStar.Logging.Extensions;

/// <summary>
///
/// </summary>
public class AStarTelemetryInitializer : ITelemetryInitializer
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="telemetry"></param>
    public void Initialize(ITelemetry telemetry)
    {
        if(string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
        {
            //set custom role name here
            telemetry.Context.Cloud.RoleName = "Custom RoleName";
            telemetry.Context.Cloud.RoleInstance = "Custom RoleInstance";
        }
    }
}
