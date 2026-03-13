using Education.Patterns.Behavioral.State.CustomStates;

namespace Education.Patterns.Behavioral.State;

// Объект меняет класс своего состояния на лету (из Tired в Energetic)
// Один и тот же метод RequestGoToShop() дает разный результат
// Пользователь класса Context не знает, какое сейчас состояние, он просто взаимодействует с человеком
public class StatePattern
{
    public void Start()
    {
        var man = new Context(new TiredState());

        man.RequestGoToShop(); // Не пойду
        man.RequestBeer();     // О, пиво (и изменит состояние на Energetic)
        man.RequestGoToShop(); // Уже бегу (поведение изменилось)
    }
}
