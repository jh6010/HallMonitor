
using HallMonitor.Core;

namespace HallMonitor.Devices;

public class TempetureSensor : Sensor
{
    public TempetureSensor(string name, double limit) : base(name, limit) {}

    protected override Reading Measure()
    {
        return new Reading
            {
                SensorName = Name,
                Value = 20 + Random.Shared.NextDouble() * 7,
                Units = "C°",
                TimeOfReading = DateTime.Now,
                Limit = Limit
            };
    }
}