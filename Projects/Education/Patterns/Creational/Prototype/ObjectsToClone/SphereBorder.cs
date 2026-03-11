using Education.Patterns.Creational.Prototype.Interfaces;

namespace Education.Patterns.Creational.Prototype.ObjectsToClone;

public class SphereBorder : IClone
{
    public string? Color { get; set; }

    public IClone Clone()
    {
        var border = new SphereBorder
        {
            Color = this.Color
        };

        return border;
    }
}
