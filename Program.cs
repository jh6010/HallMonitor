using HallMonitor.Core;
using HallMonitor.Devices;
using HallMonitor.UI;

namespace HallMonitor;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Hall Monitor ===");

        List<Device> devices =
        [
            new Fan("fan_1", 1000),
            new Fan("fan_2", 1300),
            new Heater("heater_1", 2.2),
            new TempetureSensor("sensor_1", 26),
            new TempetureSensor("sensor_2", 26.5),
        ];

        Hall hall = new();
        foreach(Device device in devices)
        {
            hall.AddDevice(device);
        }

        Menu menu = new(hall);
        while(menu.Present());
    }
}