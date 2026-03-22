namespace backend_agua.Dtos.Reporte;

public class ReporteStatusDto
{
    public bool IsOpen { get; set; }
    public DateTime ServerTime { get; set; }
    public DateTime NextOpeningTime { get; set; }
    public DateTime? ClosingTime { get; set; }
    public double RemainingSeconds { get; set; }
    public string Message { get; set; } = string.Empty;
}
