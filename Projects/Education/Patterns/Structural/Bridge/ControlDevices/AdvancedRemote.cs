using Education.Patterns.Structural.Bridge.Interfaces;

namespace Education.Patterns.Structural.Bridge.ControlDevices;

public class AdvancedRemote : RemoteControl
{
    public AdvancedRemote(IDevice device)
     : base(device)
    {
    }

    // Дополительная функция, плюс к тем, что есть в базовом классе
    public void Mute()
    {
        Console.WriteLine("Звук выключен");
    }
}
