using Education.Patterns.Structural.Bridge.Interfaces;

namespace Education.Patterns.Structural.Bridge.Devices;

public class TV : IDevice
{
    public void Enable()
    {
        Console.WriteLine("Телевизор включен");
    }

    public void Disable()
    {
        Console.WriteLine("Телевизор выключен");
    }
}
