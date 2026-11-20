using System;
using System.Xml.Serialization;

namespace lab5
{
    [XmlInclude(typeof(Vegetable))]
    [XmlInclude(typeof(OtherIngredient))]
    public abstract class Ingredient
    {
        protected Ingredient() { }

        protected Ingredient(string name, double weight, double caloriesPer100g)
        {
            Name = name;
            Weight = weight;
            CaloriesPer100g = caloriesPer100g;
        }

        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double CaloriesPer100g { get; set; }

        [XmlIgnore]
        public double TotalCalories => Weight / 100.0 * CaloriesPer100g;

        public abstract void Edit();

        public override string ToString() =>
            $"{Name} — {Weight:F1} г, {CaloriesPer100g:F1} ккал/100г, всего {TotalCalories:F1} ккал";
    }
}