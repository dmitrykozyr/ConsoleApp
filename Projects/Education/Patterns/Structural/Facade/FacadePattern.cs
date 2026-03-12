using Education.Patterns.Structural.Facade.SubSystems;

namespace Education.Patterns.Structural.Facade;

// Если-бы для запуска авто нужно было выполнить кучу действий,
// это было-бы сложно, поэтому все происходит под капотом после поворота ключа
public class FacadePattern
{
    public void Start()
    {
        var subSystem_1 = new SubSystem_1();
        var subSystem_2 = new SubSystem_2();

        var facade = new Facade(subSystem_1, subSystem_2);

        facade.StartEngine();
    }
}
