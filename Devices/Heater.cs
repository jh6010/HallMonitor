
using HallMonitor.Core;

namespace HallMonitor.Devices;

public class Heater : Device
{
    private double _powerOutput;

    public double MaxPowerOutput {get;}

    public Heater(string name, double maxPowerOutput) : base(name) {
        MaxPowerOutput = maxPowerOutput;
        _powerOutput = 0;
    }

    public override void Start() 
    {
        _running = true;
        _powerOutput = MaxPowerOutput;
    }

    public override void Stop()
    {
        _running = false;
        _powerOutput = 0;
    }

    public override string StateToString()
    {
        if(_running)
        {
            return $"{Name}: heating at {_powerOutput} kW";
        }
        else
        {
            return $"{Name}: not heating";
        }
    }
}