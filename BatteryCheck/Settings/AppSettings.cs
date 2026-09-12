namespace BatteryCheck.Settings;

public sealed class AppSettings
{
    public bool StartWithWindows { get; set; }
    public bool RememberWindowSize { get; set; } = true;
    public bool RememberWindowPosition { get; set; } = true;
    public int RefreshIntervalSeconds { get; set; } = 60;
    public bool HasSavedWindowBounds { get; set; }
    public int WindowX { get; set; }
    public int WindowY { get; set; }
    public int WindowWidth { get; set; } = 520;
    public int WindowHeight { get; set; } = 330;
}
