namespace HallMonitor.Core;

public record Reading
{
    public string SensorName;
    public double Value;
    public double Limit;
    public string Units;
    public DateTime TimeOfReading;

    public bool IsEmpty => string.IsNullOrEmpty(SensorName)
                        || string.IsNullOrEmpty(Units);

    public override string ToString() => $"{TimeOfReading.ToString("HH:mm:ss")} {Value.ToString("0.0")} {Units}, limit {Limit.ToString("0.0")} {Units}";
}
