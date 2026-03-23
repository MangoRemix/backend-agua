namespace backend_agua.Settings;

public class ReportSettings
{
    public int OpenHour { get; set; } = 20;
    public int OpenMinute { get; set; } = 0;
    public int DurationMinutes { get; set; } = 60;
    public bool IsManualEnabled { get; set; } = false;
    public bool IsManualDisabled { get; set; } = false;
}
