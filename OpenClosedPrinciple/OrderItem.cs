namespace OpenClosedPrinciple
{
    // Класс содержит общую информацию для классов Meal и Beverage
    // Он абстрактный и имеет абстрактный метод
    public abstract class OrderItem : IPriority
    {
        public string Name { get; set; }

        public int Priority { get; set; }

        public abstract void SetPriority();
    }
}
