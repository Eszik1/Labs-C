using System;

namespace lab5
{
    public class Vegetable : Ingredient
    {
        public Vegetable() { }

        public Vegetable(string name, double weight, double caloriesPer100g, string color)
            : base(name, weight, caloriesPer100g)
        {
            Color = color;
        }

        public string Color { get; set; } = string.Empty;

        public override void Edit()
        {
            Console.Write("Новое название (Enter без изменений): ");
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name.Trim();
            }

            Console.Write("Новый цвет (Enter без изменений): ");
            string? color = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(color))
            {
                Color = color.Trim();
            }

            double? newWeight = InputHelper.ReadOptionalDouble("Новый вес в г (Enter без изменений): ");
            if (newWeight.HasValue)
            {
                Weight = newWeight.Value;
            }

            double? newKcal = InputHelper.ReadOptionalDouble("Новая калорийность на 100г (Enter без изменений): ");
            if (newKcal.HasValue)
            {
                CaloriesPer100g = newKcal.Value;
            }
        }

        public override string ToString() =>
            $"{base.ToString()} (Овощ, цвет: {Color})";
    }
}