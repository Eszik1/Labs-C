using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

struct GeneticData
{
    public string protein;
    public string organism;
    public string amino_acids;
}

internal class Program
{
    static string RLDecoding(string amino_acids)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < amino_acids.Length; i++)
        {
            char current = amino_acids[i];

            if (char.IsDigit(current) && i + 1 < amino_acids.Length)
            {
                int count = current - '0';
                char nextChar = amino_acids[i + 1];
                result.Append(nextChar, count);
                i++;
            }
            else
            {
                result.Append(current);
            }
        }
        return result.ToString();
    }

    static List<GeneticData> ReadSequences(string filePath)
    {
        List<GeneticData> list = new List<GeneticData>();

        if (!File.Exists(filePath))
            return list;

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split('\t');

            if (parts.Length >= 3)
            {
                GeneticData data = new GeneticData();
                data.protein = parts[0].Trim();
                data.organism = parts[1].Trim();
                data.amino_acids = RLDecoding(parts[2].Trim());

                list.Add(data);
            }
        }

        return list;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Запуск программы генетического поиска...");


        List<GeneticData> database = ReadSequences("sequences.txt");
        Console.WriteLine($"Успешно загружено белков: {database.Count}");


        ProcessCommandsAndWriteReport(database, "commands.txt", "genedata.txt");
        Console.WriteLine("Готово! Результаты сохранены в genedata.txt");
    }
}