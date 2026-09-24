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
    static void Main(string[] args)
    {
        Console.WriteLine("Запуск программы генетического поиска...");


        List<GeneticData> database = ReadSequences("sequences.txt");
        Console.WriteLine($"Успешно загружено белков: {database.Count}");


        ProcessCommandsAndWriteReport(database, "commands.txt", "genedata.txt");
        Console.WriteLine("Готово! Результаты сохранены в genedata.txt");
    }
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
    static string ProcessSearch(List<GeneticData> database, string query)
    {
        string pattern = RLDecoding(query);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("organism\tprotein");

        bool foundAny = false;

        foreach (var data in database)
        {
            if (data.amino_acids.Contains(pattern))
            {
                sb.AppendLine($"{data.organism}\t{data.protein}");
                foundAny = true;
            }
        }

        if (!foundAny)
        {
            return "organism\tprotein\nNOT FOUND";
        }

        return sb.ToString().TrimEnd();
    }

    static string ProcessDiff(List<GeneticData> database, string protein1Name, string protein2Name)
    {
        GeneticData? g1 = null;
        GeneticData? g2 = null;

        foreach (var data in database)
        {
            if (data.protein == protein1Name) g1 = data;
            if (data.protein == protein2Name) g2 = data;
        }

        bool p1Missing = (g1 == null);
        bool p2Missing = (g2 == null);

        if (p1Missing || p2Missing)
        {
            StringBuilder missingSb = new StringBuilder();
            missingSb.AppendLine("amino-acids difference:");
            if (p1Missing) missingSb.AppendLine($"MISSING: {protein1Name}");
            if (p2Missing) missingSb.AppendLine($"MISSING: {protein2Name}");
            return missingSb.ToString().TrimEnd();
        }

        string seq1 = g1.Value.amino_acids;
        string seq2 = g2.Value.amino_acids;

        int minLength = Math.Min(seq1.Length, seq2.Length);
        int maxLength = Math.Max(seq1.Length, seq2.Length);

        int differences = 0;

        for (int i = 0; i < minLength; i++)
        {
            if (seq1[i] != seq2[i])
            {
                differences++;
            }
        }

        differences += (maxLength - minLength);

        return $"amino-acids difference:\n{differences}";
    }

    static string ProcessMode(List<GeneticData> database, string proteinName)
    {
        GeneticData? foundData = null;
        foreach (var data in database)
        {
            if (data.protein == proteinName)
            {
                foundData = data;
                break;
            }
        }

        if (foundData == null)
        {
            return $"amino-acid occurs:\nMISSING: {proteinName}";
        }

        string seq = foundData.Value.amino_acids;

        Dictionary<char, int> counts = new Dictionary<char, int>();
        foreach (char c in seq)
        {
            if (counts.ContainsKey(c))
                counts[c]++;
            else
                counts[c] = 1;
        }

        char bestChar = 'Z';
        int maxCount = -1;

        foreach (var pair in counts)
        {
            char currentChar = pair.Key;
            int currentCount = pair.Value;

            if (currentCount > maxCount)
            {
                maxCount = currentCount;
                bestChar = currentChar;
            }
            else if (currentCount == maxCount)
            {
                if (currentChar < bestChar)
                {
                    bestChar = currentChar;
                }
            }
        }

        return $"amino-acid occurs:\n{bestChar}\n{maxCount}";
    }
}