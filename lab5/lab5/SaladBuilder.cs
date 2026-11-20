using lab5;

namespace lab5
{
    public class SaladBuilder
    {
        private readonly Salad _salad;

        public SaladBuilder(string saladName)
        {
            _salad = new Salad(saladName);
        }

        public SaladBuilder WithVegetable(string name, double weight, double caloriesPer100g, string color)
        {
            _salad.AddIngredient(new Vegetable(name, weight, caloriesPer100g, color));
            return this;
        }

        public SaladBuilder WithOtherIngredient(
            string name,
            double weight,
            double caloriesPer100g,
            IngredientCategory category)
        {
            _salad.AddIngredient(new OtherIngredient(name, weight, caloriesPer100g, category));
            return this;
        }

        public Salad Build() => _salad;
    }
}