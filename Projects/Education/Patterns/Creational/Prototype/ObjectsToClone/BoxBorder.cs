using Education.Patterns.Creational.Prototype.Interfaces;

namespace Education.Patterns.Creational.Prototype.ObjectsToClone;

public class BoxBorder : IClone
{
    public string? Color { get; set; }

    public IClone Clone()
    {
        var border = new BoxBorder
        {
            Color = this.Color
        };

        return border;
    }
}
