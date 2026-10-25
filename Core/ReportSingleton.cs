using System.Diagnostics;

namespace HallMonitor.Core;

public class ReportSingleton
{
    private static ReportSingleton _instance;

    private Stopwatch _stopwatch = new();
    private int _measurementCount;

    public void StartTimer() => _stopwatch.Start();
    public void IncrementMeasurementCount() => Interlocked.Increment(ref _measurementCount);

    private ReportSingleton() {}

    public static ReportSingleton Instance {
        get
        {
            if(_instance == null)
            {
                _instance = new ReportSingleton();
            }
            return _instance;
        }
    }

    public string Report => @$"
=== Report ===
Measurements made: {_measurementCount}
Program ran for: {_stopwatch.Elapsed}
";

}