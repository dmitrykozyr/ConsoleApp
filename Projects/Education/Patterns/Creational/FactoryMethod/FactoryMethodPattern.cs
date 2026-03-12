using Education.Patterns.Creational.FactoryMethod.CustomDevelopers;
using Education.Patterns.Creational.FactoryMethod.Interfaces;

namespace Education.Patterns.Creational.FactoryMethod;

public class FactoryMethodPattern
{
    // Есть фабрика постройки деревянных и панельных домов
    // Можем легко добавить новый тип не через new, а через фабричный метод,
    // который переопределяем для получения нужного типа объекта
    public void Start()
    {
        IDeveloper developer;

        // Деревянный дом
        developer = new WoodDeveloper();
        IHouse woodHouse = developer.FactoryMethod();
        woodHouse.Construct("Антон");

        // Панельный дом
        developer = new PanelDeveloper();
        IHouse panelHouse = developer.FactoryMethod();
        panelHouse.Construct("Анастасия");
    }
}
