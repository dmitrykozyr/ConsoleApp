namespace Education.General.GoodPractices;

public class SOLID_
{
    // Single Responsibility    большие классы разделять на малые, чтобы каждый выполнял одну задачу
    // Open Closed              методы класса должны быть открыты для расширения, но закрыты для изменения
    // Liskov Substitution      объекты можно заменить их наследниками без изменения свойств программы
    // Interface Segregation    не создавать интерфейсы с большим числом методов
    // Dependency Invertion     зависимости кода должны строиться от абстракции

    #region Liskov Substitution

    // Класс FrozenDeposit нарушает принцип подстановки Лисков
    // Объекты подкласса должны быть заменяемыми объектами базового класса без изменения желаемых свойств программы

    public abstract class Deposit
    {
        public decimal Balance { get; protected set; }

        public virtual void Credit(decimal amount)
        {
            Balance += amount;
        }
    }

    public class FrozenDeposit : Deposit
    {
        public override void Credit(decimal amount)
        {
            throw new Exception("This deposit does not support filling up");
        }
    }

    #endregion

    #region DependencyInvertion

    interface IDependencyInvertion
    {
        void F1();
    }

    class A : IDependencyInvertion
    {
        public void F1()
        {
            Console.WriteLine("A");
        }
    }

    class B : IDependencyInvertion
    {
        public void F1()
        {
            Console.WriteLine("B");
        }
    }

    class C
    {
        private readonly IDependencyInvertion _di;

        public C(IDependencyInvertion di)
        {
            _di = di;
        }

        public void F2()
        {
            _di.F1();
        }
    }

    static void Main_()
    {
        IDependencyInvertion dependencyInvertion = new A(); // A меняем на B при необходимости
        dependencyInvertion.F1();

        var c = new C(dependencyInvertion);
        c.F2();
    }

    #endregion
}
