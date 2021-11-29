using System;
using System.Collections.Generic;

namespace OpenClosedPrinciple
{
    class Program
    {
        static void Main()
        {
            var order = new List<OrderItem>()
            {
                new Meal() { Name = "Steak", Type = Meal.MealType.MainDish},
                new Meal() { Name = "Cake", Type = Meal.MealType.Dessert },
                new Beverage() { Name = "Beer", Type = Beverage.BeverageType.Alcoholic, Size = Beverage.BeverageSize.Normal }
            };

            order.ForEach(x => x.SetPriority());

            Console.WriteLine("Order priority has been set");
        }
    }
}
