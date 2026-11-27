using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DatPaper.Extension;

public static class SessionExtensions
{
    public static void Set<T>(
        this ISession session, string key, T value)
    {
        var json = JsonConvert.SerializeObject(
            value,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore
            });

        session.SetString(key, json);
    }

    public static T Get<T>(
        this ISession session, string key) where T : new()
    {
        var json = session.GetString(key);

        if (string.IsNullOrWhiteSpace(json))
            return new T();

        var result = JsonConvert.DeserializeObject<T>(json);

        return result is null ? new T() : result;
    }
}