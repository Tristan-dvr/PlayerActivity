using BepInEx.Logging;
using System;

class Log
{
    private static Log _instance;

    private ManualLogSource _source;

    public static Log CreateInstance(ManualLogSource source)
    {
        _instance = new Log
        {
            _source = source,
        };
        return _instance;
    }

    private Log() { }

    public static void Info(object msg) => _instance._source.LogInfo(FormatMessage(msg));

    public static void Message(object msg) => _instance._source.LogMessage(FormatMessage(msg));

    public static void Debug(object msg) => _instance._source.LogDebug(FormatMessage(msg));

    public static void Warning(object msg) => _instance._source.LogWarning(FormatMessage(msg));

    public static void Error(object msg) => _instance._source.LogError(FormatMessage(msg));

    public static void Fatal(object msg) => _instance._source.LogFatal(FormatMessage(msg));

    private static string FormatMessage(object msg) => $"[{DateTime.UtcNow}] {msg}";
}
