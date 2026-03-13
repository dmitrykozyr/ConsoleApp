using Education.Patterns.Structural.Bridge.Interfaces;

namespace Education.Patterns.Structural.Bridge.Devices;

public class Radio : IDevice
{
    public void Enable()
    {
        Console.WriteLine("Радио включено");
    }

    public void Disable()
    {
        Console.WriteLine("Радио выключено");
    }
}
