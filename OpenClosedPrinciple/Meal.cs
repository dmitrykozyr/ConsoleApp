namespace OpenClosedPrinciple
{
    public class Meal : OrderItem
    {
        public MealType Type { get; set; }

        public enum MealType
        {
            MainDish,
            Dessert,
            Salad,
        }

        // Переопределяем абстрактный метод
        public override void SetPriority()
        {
            switch (Type)
            {
                case MealType.Salad:    Priority = 1; break;
                case MealType.MainDish: Priority = 2; break;
                case MealType.Dessert:  Priority = 3; break;
            }
        }
    }
}
