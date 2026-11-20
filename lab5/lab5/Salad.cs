using lab5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace lab5
{
    public enum SortParameter
    {
        TotalCalories = 1,
        CaloriesPer100g = 2,
        Weight = 3
    }

    public class Salad
    {
        public Salad()
        {
            Ingredients = new List<Ingredient>();
        }

        public Salad(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
        }

        public string Name { get; set; } = string.Empty;
        public List<Ingredient> Ingredients { get; set; }

        [XmlIgnore]
        public double TotalCalories => Ingredients.Sum(ingredient => ingredient.TotalCalories);

        [XmlIgnore]
        public double TotalWeight => Ingredients.Sum(ingredient => ingredient.Weight);

        public void AddIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                Ingredients.Add(ingredient);
            }
        }

        public void RemoveIngredientAt(int index)
        {
            if (index >= 0 && index < Ingredients.Count)
            {
                Ingredients.RemoveAt(index);
            }
        }

        public void ShowIngredients()
        {
            if (Ingredients.Count == 0)
            {
                Console.WriteLine("Салат пуст.");
                return;
            }

            Console.WriteLine($"\nИнгредиенты салата \"{Name}\":");
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Ingredients[i]}");
            }
        }

        public void SortVegetables(SortParameter parameter)
        {
            List<Vegetable> vegetables = Ingredients.OfType<Vegetable>().ToList();
            if (vegetables.Count == 0)
            {
                return;
            }

            List<Vegetable> sortedVegetables = parameter switch
            {
                SortParameter.CaloriesPer100g => vegetables.OrderBy(v => v.CaloriesPer100g).ToList(),
                SortParameter.Weight => vegetables.OrderBy(v => v.Weight).ToList(),
                _ => vegetables.OrderBy(v => v.TotalCalories).ToList()
            };

            Queue<Vegetable> queue = new Queue<Vegetable>(sortedVegetables);
            for (int i = 0; i < Ingredients.Count; i++)
            {
                if (Ingredients[i] is Vegetable)
                {
                    Ingredients[i] = queue.Dequeue();
                }
            }
        }

        public void SortAllIngredients(SortParameter parameter)
        {
            Ingredients = parameter switch
            {
                SortParameter.CaloriesPer100g => Ingredients.OrderBy(i => i.CaloriesPer100g).ToList(),
                SortParameter.Weight => Ingredients.OrderBy(i => i.Weight).ToList(),
                _ => Ingredients.OrderBy(i => i.TotalCalories).ToList()
            };
        }

        public List<Vegetable> FindVegetablesByCaloriesRange(double minCalories, double maxCalories)
        {
            return Ingredients
                .OfType<Vegetable>()
                .Where(v => v.TotalCalories >= minCalories && v.TotalCalories <= maxCalories)
                .ToList();
        }

        public override string ToString() =>
            $"{Name} — {Ingredients.Count} ингредиентов, {TotalWeight:F1} г, {TotalCalories:F1} ккал";
    }
}