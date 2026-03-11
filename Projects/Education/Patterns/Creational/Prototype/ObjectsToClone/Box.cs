using Education.Patterns.Creational.Prototype.Interfaces;

namespace Education.Patterns.Creational.Prototype.ObjectsToClone;

public class Box : IClone
{
    public int Width;

    public BoxBorder? BoxBorder;

    public Box(int width, BoxBorder? boxBorder)
    {
        Width = width;
        BoxBorder = boxBorder;
    }

    public IClone Clone()
    {
        // Делаем поверхностную копию простых типов встроенным методом MemberwiseClone()
        var clone = (Box)this.MemberwiseClone();

        // Вручную клонируем объекты ссылочного типа,
        // чтобы каждый клон ссылался на свой участок в памяти
        clone.BoxBorder = (BoxBorder)this.BoxBorder.Clone();

        Console.WriteLine("Куб склонирован");

        return clone;
    }
}
