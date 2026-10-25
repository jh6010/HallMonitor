using System.Diagnostics;

namespace HallMonitor.Core;

public abstract class Sensor : Device
{
    public double Limit { get; }

    private Reading _lastReading;

    private readonly System.Timers.Timer _timer;

    protected abstract Reading Measure();

    public event EventHandler<Reading> LimitExceeded;
    public event EventHandler MeasurementTaken;

    public Sensor(string name, double limit) : base(name)
    {
        Limit = limit;
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTick;
    }

    public override void Start()
    {
        _running = true;
        _timer.Start();
    }

    public override void Stop()
    {
        _running = false;
        _timer.Stop();
    }

    public override string StateToString()
    {
        if(_running)
        {
            return $"{Name}: measuring {_lastReading}";
        }
        else
        {
            return $"{Name}: not measuring";
        }
    }

    private void OnTick(object sender, System.Timers.ElapsedEventArgs e)
    {
        Reading reading = Measure();
        MeasurementTaken?.Invoke(this, null);

        if(reading.IsEmpty)
        {
            throw new InvalidDataException("Reading cant be empty");
        }
        _lastReading = reading;

        if(reading.Value > Limit)
        {
            LimitExceeded?.Invoke(this, reading);
        }
    }
}