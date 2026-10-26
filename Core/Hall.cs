using System.Collections.Immutable;

namespace HallMonitor.Core;

public class Hall
{
    private List<Device> _devices = new();
    private List<Reading> _alarmHistory = new();

    public ImmutableList<Device> Devices => _devices.ToImmutableList<Device>();
    public ImmutableList<Reading> AlarmHistory => _alarmHistory.ToImmutableList<Reading>();

    public event EventHandler<Reading> AlarmRaised;

    ~Hall()
    {
        foreach(Device device in _devices)
        {
            if(device is Sensor sensor)
            {
                sensor.LimitExceeded -= SensorLimitExceeded;
            }
        }
    }

    public void AddDevice(Device device) 
    {
        _devices.Add(device);

        if(device is Sensor sensor) 
        {
            sensor.LimitExceeded += SensorLimitExceeded;
        }
    }

    public void RemoveDevice(Device device)
    {
        _devices.Remove(device);

        if(device is Sensor sensor) 
        {
            sensor.LimitExceeded -= SensorLimitExceeded;
        }
    }

    private void SensorLimitExceeded(object sender, Reading reading)
    {
        if(sender is Sensor)
        {
            AlarmRaised?.Invoke(this, reading);
            _alarmHistory.Add(reading);
        }
    }

    public void StartAll()
    {
        foreach(Device device in _devices)
        {
            device.Start();
        }
    }

    public void StopAll()
    {
        foreach(Device device in _devices)
        {
            device.Stop();
        }
    }

    public void StartDevice(int index)
    {
        if(index < 0 || index > _devices.Count - 1)
        {
            throw new InvalidOperationException("Devices index out of range");
        }
        _devices[index].Start();
    }

    public void StopDevice(int index)
    {
        if(index < 0 || index > _devices.Count - 1)
        {
            throw new InvalidOperationException("Devices index out of range");
        }
        _devices[index].Stop();
    }
}