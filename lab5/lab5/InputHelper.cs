using System;
using System.Globalization;

namespace lab5
{
    public static class InputHelper
    {
        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? raw = Console.ReadLine();
                string normalized = (raw ?? string.Empty).Trim().Replace(',', '.');

                if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out double result) &&
                    result >= 0)
                {
                    return result;
                }

                Console.WriteLine("Некорректное число. Попробуйте снова.");
            }
        }

        public static double? ReadOptionalDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? raw = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(raw))
                {
                    return null;
                }

                string normalized = raw.Trim().Replace(',', '.');
                if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out double result) &&
                    result >= 0)
                {
                    return result;
                }

                Console.WriteLine("Некорректное число или нажмите Enter для пропуска.");
            }
        }

        public static int ReadIndex(int max, string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? raw = Console.ReadLine();
                if (int.TryParse(raw?.Trim(), out int num) && num >= 1 && num <= max)
                {
                    return num;
                }

                Console.WriteLine($"Введите число от 1 до {max}.");
            }
        }

        public static SortParameter ReadSortParameter()
        {
            while (true)
            {
                Console.WriteLine("Параметр сортировки:\n1. Общая калорийность\n2. Калорийность на 100г\n3. Вес (г)");
                Console.Write("Выбор: ");
                string? raw = Console.ReadLine()?.Trim();
                switch (raw)
                {
                    case "1":
                        return SortParameter.TotalCalories;
                    case "2":
                        return SortParameter.CaloriesPer100g;
                    case "3":
                        return SortParameter.Weight;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }
    }
}