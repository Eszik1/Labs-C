using System;

namespace lab5
{
    internal class Program
    {
        private static readonly IngredientStorage IngredientStorage = new IngredientStorage();
        private static readonly SaladStorage SaladStorage = new SaladStorage(IngredientStorage);

        private static void Main(string[] args)
        {
            while (true)
            {
                ShowMainMenu();
                Console.Write("Выбор: ");
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        IngredientMenu();
                        break;
                    case "2":
                        SaladMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine("\nГЛАВНОЕ МЕНЮ \n1. Управление ингредиентами\n2. Управление салатами\n0. Выход");
        }

        private static void IngredientMenu()
        {
            while (true)
            {
                Console.WriteLine(
                    "\nИНГРЕДИЕНТЫ\n" +
                    "1. Добавить ингредиент\n" +
                    "2. Удалить ингредиент\n" +
                    "3. Редактировать ингредиент\n" +
                    "4. Показать все ингредиенты\n" +
                    "5. Сохранить ингредиенты\n" +
                    "6. Загрузить ингредиенты\n" +
                    "0. Назад");

                Console.Write("Выбор: ");
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        IngredientStorage.AddIngredient();
                        break;
                    case "2":
                        IngredientStorage.RemoveIngredient();
                        break;
                    case "3":
                        IngredientStorage.EditIngredient();
                        break;
                    case "4":
                        IngredientStorage.ShowAll();
                        break;
                    case "5":
                        IngredientStorage.Save();
                        break;
                    case "6":
                        IngredientStorage.Load();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }

        private static void SaladMenu()
        {
            while (true)
            {
                Console.WriteLine(
                    "\nСАЛАТЫ \n" +
                    "1. Создать салат\n" +
                    "2. Создать готовый салат (Builder)\n" +
                    "3. Удалить салат\n" +
                    "4. Показать все салаты\n" +
                    "5. Показать ингредиенты и калорийность\n" +
                    "6. Добавить ингредиент в салат\n" +
                    "7. Удалить ингредиент из салата\n" +
                    "8. Сортировать овощи по параметру\n" +
                    "9. Найти овощи по диапазону калорийности\n" +
                    "10. Сохранить салаты\n" +
                    "11. Загрузить салаты\n" +
                    "0. Назад");

                Console.Write("Выбор: ");
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        SaladStorage.CreateSalad();
                        break;
                    case "2":
                        SaladStorage.CreateSampleSaladWithBuilder();
                        break;
                    case "3":
                        SaladStorage.RemoveSalad();
                        break;
                    case "4":
                        SaladStorage.ShowAll();
                        break;
                    case "5":
                        SaladStorage.ShowIngredients();
                        break;
                    case "6":
                        SaladStorage.AddIngredientToSalad();
                        break;
                    case "7":
                        SaladStorage.RemoveIngredientFromSalad();
                        break;
                    case "8":
                        SaladStorage.SortSaladVegetables();
                        break;
                    case "9":
                        SaladStorage.FindVegetablesByCalories();
                        break;
                    case "10":
                        SaladStorage.Save();
                        break;
                    case "11":
                        SaladStorage.Load();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }
    }
}