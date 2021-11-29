namespace OpenClosedPrinciple
{
    public class Beverage : OrderItem
    {
        public BeverageType Type { get; set; }

        public BeverageSize Size { get; set; }

        public enum BeverageType
        {
            Alcoholic,
            NonAlcoholic
        }

        public enum BeverageSize
        {
            Small,
            Normal,
            Large
        }

        // Переопределяем абстрактный метод
        public override void SetPriority()
        {
            switch (Size)
            {
                case BeverageSize.Small:  Priority = 1; break;
                case BeverageSize.Normal: Priority = 2; break;
                case BeverageSize.Large:  Priority = 3; break;
            }
        }
    }
}
