using Education.Patterns.Structural.Facade.SubSystems;

namespace Education.Patterns.Structural.Facade;

public class Facade
{
    private readonly SubSystem_1 _subSystem_1;
    private readonly SubSystem_2 _subSystem_2;

    public Facade(SubSystem_1 subSystem_1, SubSystem_2 subSystem_2)
    {
        _subSystem_1 = subSystem_1;
        _subSystem_2 = subSystem_2;
    }

    public void StartEngine()
    {
        _subSystem_1.StartSystem();
        _subSystem_2.StartSystem();
    }
}
