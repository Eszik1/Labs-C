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
}