using lab5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace lab5
{
    [XmlInclude(typeof(Vegetable))]
    [XmlInclude(typeof(OtherIngredient))]
    public class IngredientStorage
    {
        private const string FileName = "ingredients.xml";
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public void ShowAll()
        {
            if (Ingredients.Count == 0)
            {
                Console.WriteLine("Список ингредиентов пуст.");
                return;
            }

            Console.WriteLine("\n--- СПИСОК ИНГРЕДИЕНТОВ ---");
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Ingredients[i]}");
            }
        }

        public void AddIngredient()
        {
            Console.WriteLine("\nТип ингредиента:\n1. Овощ\n2. Прочий");
            Console.Write("Выбор: ");
            string? type = Console.ReadLine()?.Trim();

            Console.Write("Название: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;

            double weight = InputHelper.ReadDouble("Вес (г): ");
            double calories = InputHelper.ReadDouble("Калорийность на 100г: ");

            Ingredient ingredient;
            if (type == "1")
            {
                Console.Write("Цвет: ");
                string color = Console.ReadLine()?.Trim() ?? string.Empty;
                ingredient = new Vegetable(name, weight, calories, color);
            }
            else
            {
                OtherIngredient.ShowCategoryMenu();
                IngredientCategory category = OtherIngredient.ReadCategory();
                ingredient = new OtherIngredient(name, weight, calories, category);
            }

            Ingredients.Add(ingredient);
            Console.WriteLine("Ингредиент добавлен.");
        }

        public void RemoveIngredient()
        {
            ShowAll();
            if (Ingredients.Count == 0)
            {
                return;
            }

            int index = InputHelper.ReadIndex(Ingredients.Count, "Номер для удаления: ");
            Ingredients.RemoveAt(index - 1);
            Console.WriteLine("Удалено.");
        }

        public void EditIngredient()
        {
            ShowAll();
            if (Ingredients.Count == 0)
            {
                return;
            }

            int index = InputHelper.ReadIndex(Ingredients.Count, "Номер для редактирования: ");
            Ingredients[index - 1].Edit();
            Console.WriteLine("Изменено.");
        }

        public void Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
                using FileStream fileStream = new FileStream(FileName, FileMode.Create);
                serializer.Serialize(fileStream, Ingredients);
                Console.WriteLine("Ингредиенты сохранены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении: " + ex.Message);
            }
        }

        public void Load()
        {
            if (!File.Exists(FileName))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
                using FileStream fileStream = new FileStream(FileName, FileMode.Open);
                Ingredients = (List<Ingredient>?)serializer.Deserialize(fileStream) ?? new List<Ingredient>();
                Console.WriteLine("Ингредиенты загружены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка загрузки: " + ex.Message);
            }
        }
    }
}