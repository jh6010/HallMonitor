
using HallMonitor.Core;

namespace HallMonitor.Devices;

public class Fan : Device
{
    private double _rpm;

    public double MaxRpm {get;}

    public Fan(string name, double maxRpm) : base(name) {
        MaxRpm = maxRpm;
        _rpm = 0;
    }

    public override void Start()
    {
        _running = true;
        _rpm = MaxRpm;
    }

    public override void Stop()
    {
        _running = false;
        _rpm = 0;
    }

    public override string StateToString()
    {
        if(_running)
        {
            return $"{Name}: running at {_rpm} RPM";
        }
        else
        {
            return $"{Name}: not running";
        }
    }
}