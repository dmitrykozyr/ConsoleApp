using Education.Patterns.Creational.Prototype.Interfaces;
using Education.Patterns.Creational.Prototype.ObjectsToClone;

namespace Education.Patterns.Creational.Prototype;

// Создаем прототип и на его онове клоны
public class PrototypePattern
{
    public void Start()
    {
        IClone boxPrototype = new Box(20, new BoxBorder { Color = "Red" });
        IClone boxClone1 = boxPrototype.Clone();
        IClone boxClone2 = boxPrototype.Clone();

        Console.WriteLine();

        IClone spherePrototype = new Sphere(30, new SphereBorder { Color = "Blue" });
        IClone sphereCloned1 = spherePrototype.Clone();
        IClone sphereCloned2 = spherePrototype.Clone();
    }
}
