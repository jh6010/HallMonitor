using System.Collections.Immutable;
using HallMonitor.Core;

namespace HallMonitor.UI;

public class Menu 
{
    private Hall _hall;

    public Menu(Hall hall)
    {
        _hall = hall;
        _hall.AlarmRaised += AlarmRaised;
    }

    ~Menu()
    {
        _hall.AlarmRaised -= AlarmRaised;
    }

    private void AlarmRaised(object sender, Reading reading)
    {
        if(sender is Hall)
        {
            Console.WriteLine($"{Environment.NewLine}! ALARM !{Environment.NewLine}{reading.SensorName}: {reading.ToString()}");
            Console.Write(_cursor);
        }
    }

    public bool Present()
    {
        int command = MakeUserChooseCommand();

        switch(command)
        {
            case 0:
                Console.WriteLine(ReportSingleton.Instance.Report);
                return false;
            
            case 1:
                ImmutableList<Device> devices = _hall.Devices;
                foreach(Device device in devices)
                {
                    Console.WriteLine(device.StateToString());
                }
                return true;
            
            case 2:
                devices = _hall.Devices;
                Console.WriteLine("Select which device to start");
                PresentDeviceMenu(devices);
                int userInput = MakeUserChooseDevice(devices);
                if(userInput == devices.Count + 1)
                {
                    _hall.StartAll();
                    Console.WriteLine("Started devices");
                }
                else if(userInput != 0)
                {
                    _hall.StartDevice(userInput - 1);
                    Console.WriteLine("Started device");
                }
                return true;

            case 3:
                devices = _hall.Devices;
                Console.WriteLine("Select which device to stop");
                PresentDeviceMenu(devices);
                userInput = MakeUserChooseDevice(devices);
                if(userInput == devices.Count + 1)
                {
                    _hall.StopAll();
                    Console.WriteLine("Stoped devices");
                }
                else  if(userInput != 0)
                {
                    _hall.StopDevice(userInput - 1);
                    Console.WriteLine("Stoped device");
                }
                return true;

            case 4:
                ImmutableList<Reading> alarmHistory = _hall.AlarmHistory;
                if(alarmHistory.Count == 0)
                {
                    Console.WriteLine("Alarm history is empty");
                    return true;
                }
                foreach(Reading alarm in alarmHistory)
                {
                    Console.WriteLine($"{alarm.SensorName}: {alarm.ToString()}");
                }
                return true;
            
            default:
                return false;
        }
    }

    private int MakeUserChooseCommand()
    {
        while(true) 
        {
            Console.Write(_cursor);
            string userInput = Console.ReadLine();
            int userInputInt = -1;

            if(userInput is "h")
            {
                Console.WriteLine(Help);
                continue;
            }

            if(!int.TryParse(userInput, out userInputInt)) 
            {
                Console.WriteLine($"Please input a number{Environment.NewLine}Input h for help");
                continue;
            }

            if(userInputInt < 0 || userInputInt > 4)
            {
                Console.WriteLine($"Unknown command{Environment.NewLine}Input h for help");
                continue;
            }

            return userInputInt;
        }
    }

    private void PresentDeviceMenu(ImmutableList<Device> devices)
    {
        Console.WriteLine($"    0 - cancel");
        for(int i = 0; i < devices.Count; i++)
        {
            Console.WriteLine($"    {i + 1} - {devices[i].StateToString()}");
        }
        Console.WriteLine($"    {devices.Count + 1} - all");
    }

    private int MakeUserChooseDevice(ImmutableList<Device> devices)
    {
        while(true) 
        {
            Console.Write(_cursor);
            string userInput = Console.ReadLine();
            int userInputInt = -1;

            if(!int.TryParse(userInput, out userInputInt)) 
            {
                Console.WriteLine("Please input a number");
                continue;
            }

            if(userInputInt < 0 || userInputInt > devices.Count + 1)
            {
                Console.WriteLine("Input out of range");
                continue;
            }

            return userInputInt;
        }
    }

    private string _cursor = "> ";

    public string Help => @$"List of commands:
    > 0 - exits the program
    > 1 - prints the status off all devices
    > 2 - presents a menu to start any device or all devices
    > 3 - presents a menu to stop any device or all devices
    > 4 - prints the alarm history{Environment.NewLine}";
}