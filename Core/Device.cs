namespace HallMonitor.Core;

public abstract class Device
{
    public readonly string Name;

    protected bool _running;
    
    public abstract void Start();
    public abstract void Stop();
    public abstract string StateToString();

    public Device(string name) {
        Name = name;
        _running = false;
    }
}