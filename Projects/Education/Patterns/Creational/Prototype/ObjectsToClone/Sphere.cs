using Education.Patterns.Creational.Prototype.Interfaces;

namespace Education.Patterns.Creational.Prototype.ObjectsToClone;

public class Sphere : IClone
{
    public int Radius;

    public SphereBorder? SphereBorder;

    public Sphere(int radius, SphereBorder? sphereBorder)
    {
        Radius = radius;
        SphereBorder = sphereBorder;
    }

    public IClone Clone()
    {
        // Делаем поверхностную копию простых типов встроенным методом MemberwiseClone()
        var clone = (Sphere)this.MemberwiseClone();

        // Вручную клонируем объекты ссылочного типа,
        // чтобы каждый клон ссылался на свой участок в памяти
        clone.SphereBorder = (SphereBorder)this.SphereBorder.Clone();

        Console.WriteLine("Сфера склонирована");

        return clone;
    }
}
