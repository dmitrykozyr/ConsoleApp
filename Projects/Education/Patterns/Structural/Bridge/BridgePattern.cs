using Education.Patterns.Structural.Bridge.ControlDevices;
using Education.Patterns.Structural.Bridge.Devices;

namespace Education.Patterns.Structural.Bridge;

// Есть разные устройства (Телевизор, Радио) и разные пульты
// С Bridge можно легко добавить новые устройства и новые пульты с доп. функциями,
// соединить их через мост _device и не нужно создавать новый пульт под каждое новое устройство
public class BridgePattern
{
    public void Start()
    {
        var tv = new TV();
        var radio = new Radio();

        var remoteControl = new RemoteControl(tv);
        var advancedRemote = new AdvancedRemote(radio);

        // Управляем ТВ обычным пультом
        remoteControl.TogglePower();

        // Управляем Радио продвинутым пультом
        // с доп. функцией изменения громкости
        advancedRemote.TogglePower();
        advancedRemote.Mute();
    }
}
