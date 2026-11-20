using System;

namespace Lab5
{
    public class OtherIngredient : Ingredient
    {
        public OtherIngredient() { }

        public OtherIngredient(
            string name,
            double weight,
            double caloriesPer100g,
            IngredientCategory category)
            : base(name, weight, caloriesPer100g)
        {
            Category = category;
        }

        public IngredientCategory Category { get; set; }

        public override void Edit()
        {
            Console.Write("Новое название (Enter без изменений): ");
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name.Trim();
            }

            Console.WriteLine("Выберите новую категорию (Enter без изменений):");
            ShowCategoryMenu();
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) &&
                int.TryParse(input.Trim(), out int num) &&
                num >= 1 &&
                num <= Enum.GetValues(typeof(IngredientCategory)).Length)
            {
                Category = (IngredientCategory)(num - 1);
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

        public static IngredientCategory ReadCategory()
        {
            IngredientCategory[] categories = (IngredientCategory[])Enum.GetValues(typeof(IngredientCategory));
            while (true)
            {
                Console.Write("Выберите категорию (номер): ");
                string? raw = Console.ReadLine();
                if (int.TryParse(raw?.Trim(), out int num) &&
                    num >= 1 &&
                    num <= categories.Length)
                {
                    return categories[num - 1];
                }

                Console.WriteLine("Некорректный ввод.");
            }
        }

        public static void ShowCategoryMenu()
        {
            int index = 1;
            foreach (IngredientCategory category in Enum.GetValues(typeof(IngredientCategory)))
            {
                Console.WriteLine($"{index}. {category}");
                index++;
            }
        }

        public override string ToString() =>
            $"{base.ToString()} (Категория: {Category})";
    }
}