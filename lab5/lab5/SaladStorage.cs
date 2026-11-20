using lab5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace lab5
{
    public class SaladStorage
    {
        private const string FileName = "salads.xml";
        public List<Salad> Salads { get; set; } = new List<Salad>();
        public IngredientStorage IngredientStorage { get; }

        public SaladStorage(IngredientStorage ingredientStorage)
        {
            IngredientStorage = ingredientStorage;
        }

        public void ShowAll()
        {
            if (Salads.Count == 0)
            {
                Console.WriteLine("Список салатов пуст.");
                return;
            }

            Console.WriteLine("\n--- СПИСОК САЛАТОВ ---");
            for (int i = 0; i < Salads.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Salads[i]}");
            }
        }

        public void CreateSalad()
        {
            Console.Write("Название салата: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;
            Salads.Add(new Salad(name));
            Console.WriteLine("Салат создан.");
        }

        public void CreateSampleSaladWithBuilder()
        {
            Salad sample = new SaladBuilder("Лёгкий овощной")
                .WithVegetable("Огурец", 120, 15, "зелёный")
                .WithVegetable("Помидор", 150, 20, "красный")
                .WithVegetable("Болгарский перец", 80, 27, "жёлтый")
                .WithOtherIngredient("Оливковое масло", 15, 884, IngredientCategory.Масло)
                .Build();

            Salads.Add(sample);
            Console.WriteLine($"Добавлен салат: {sample}");
        }

        private Salad? SelectSalad()
        {
            ShowAll();
            if (Salads.Count == 0)
            {
                return null;
            }

            int index = InputHelper.ReadIndex(Salads.Count, "Номер салата: ");
            return Salads[index - 1];
        }

        public void RemoveSalad()
        {
            Salad? salad = SelectSalad();
            if (salad == null)
            {
                return;
            }

            Salads.Remove(salad);
            Console.WriteLine("Салат удалён.");
        }

        public void ShowIngredients()
        {
            Salad? salad = SelectSalad();
            if (salad == null)
            {
                return;
            }

            salad.ShowIngredients();
            Console.WriteLine($"Итого: {salad.TotalWeight:F1} г, {salad.TotalCalories:F1} ккал.");
        }

        public void AddIngredientToSalad()
        {
            Salad? salad = SelectSalad();
            if (salad == null)
            {
                return;
            }

            IngredientStorage.ShowAll();
            if (IngredientStorage.Ingredients.Count == 0)
            {
                Console.WriteLine("Нет ингредиентов для добавления.");
                return;
            }

            int index = InputHelper.ReadIndex(IngredientStorage.Ingredients.Count, "Номер ингредиента: ");
            salad.AddIngredient(IngredientStorage.Ingredients[index - 1]);
            Console.WriteLine("Ингредиент добавлен в салат.");
        }

        public void RemoveIngredientFromSalad()
        {
            Salad? salad = SelectSalad();
            if (salad == null)
            {
                return;
            }

            salad.ShowIngredients();
            if (salad.Ingredients.Count == 0)
            {
                return;
            }

            int index = InputHelper.ReadIndex(salad.Ingredients.Count, "Номер ингредиента для удаления: ");
            salad.RemoveIngredientAt(index - 1);
            Console.WriteLine("Ингредиент удалён.");
        }

        public void SortSaladVegetables()
        {
            Salad? salad = SelectSalad();
            if (salad == null)
            {
                return;
            }

            SortParameter param = InputHelper.ReadSortParameter();
            salad.SortVegetables(param);
            Console.WriteLine("Овощи в салате отсортированы.");
            salad.ShowIngredients();
        }

        public void FindVegetablesByCalories()
        {
            Salad? salad = SelectSalad();
            if (salad == null)
            {
                return;
            }

            double minKcal = InputHelper.ReadDouble("Минимальная калорийность (ккал): ");
            double maxKcal = InputHelper.ReadDouble("Максимальная калорийность (ккал): ");

            if (minKcal > maxKcal)
            {
                (minKcal, maxKcal) = (maxKcal, minKcal);
            }

            List<Vegetable> found = salad.FindVegetablesByCaloriesRange(minKcal, maxKcal);
            if (found.Count == 0)
            {
                Console.WriteLine($"Овощей в диапазоне [{minKcal:F1}; {maxKcal:F1}] ккал не найдено.");
                return;
            }

            Console.WriteLine($"\nНайденные овощи ({minKcal:F1}–{maxKcal:F1} ккал):");
            foreach (Vegetable veg in found)
            {
                Console.WriteLine($"- {veg}");
            }
        }

        public void Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Salad>));
                using FileStream fileStream = new FileStream(FileName, FileMode.Create);
                serializer.Serialize(fileStream, Salads);
                Console.WriteLine("Салаты сохранены.");
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Salad>));
                using FileStream fileStream = new FileStream(FileName, FileMode.Open);
                Salads = (List<Salad>?)serializer.Deserialize(fileStream) ?? new List<Salad>();
                Console.WriteLine("Салаты загружены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка загрузки: " + ex.Message);
            }
        }
    }
}