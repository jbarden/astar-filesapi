namespace AStar.Utilities;

/// <summary>
///
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T ParseEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);
}
