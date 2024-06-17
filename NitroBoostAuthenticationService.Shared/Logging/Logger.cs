namespace NitroBoostAuthenticationService.Shared.Logging;

public static class Logger
{
    public static void Log(string input, Severity severity = Severity.Default)
    {
        Console.ForegroundColor = GetFontColor(severity);
        Console.WriteLine($"{GetHeader(severity)}{input}");
    }

    private static ConsoleColor GetFontColor(Severity severity) => severity switch
    {
        Severity.Default => ConsoleColor.White,
        Severity.Info => ConsoleColor.Cyan,
        Severity.Warning => ConsoleColor.Yellow,
        Severity.Error => ConsoleColor.Red,
        Severity.Critical => ConsoleColor.DarkRed,
        _ => ConsoleColor.White
    };

    private static string GetHeader(Severity severity) => severity switch
    {
        Severity.Default => $"[DFLT - {DateTime.Now.ToString("s")}] - ",
        Severity.Info => $"[INFO - {DateTime.Now.ToString("s")}] - ",
        Severity.Warning => $"[WARN - {DateTime.Now.ToString("s")}] - ",
        Severity.Error => $"[EXCP - {DateTime.Now.ToString("s")}] - ",
        Severity.Critical => $"[CRIT - {DateTime.Now.ToString("s")}] - ",
        _ => ""
    };
}

public enum Severity
{
    Default,
    Info,
    Warning,
    Error,
    Critical
}