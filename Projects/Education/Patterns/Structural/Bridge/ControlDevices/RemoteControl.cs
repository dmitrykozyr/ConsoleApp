using Education.Patterns.Structural.Bridge.Interfaces;

namespace Education.Patterns.Structural.Bridge.ControlDevices;

public class RemoteControl
{
    // Мост к устройству
    protected IDevice _device;

    public RemoteControl(IDevice device)
    {
        _device = device;
    }

    public void TogglePower()
    {
        Console.WriteLine("Нажата кнопка питания");

        _device.Enable();
    }
}
