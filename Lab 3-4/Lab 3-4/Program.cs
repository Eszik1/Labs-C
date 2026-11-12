using System;
using System.Collections.Generic;
using System.Text;
using laba3_4;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        string sampleText =
            "Peter Piper picked a peck of pickled peppers.\n" +
            "A peck of pickled peppers Peter Piper picked.\n" +
            "Cat dog bat run. Peter Piper picked a peck of pickled peppers."+
            "If Peter Piper picked a peck of pickled peppers, where is the peck that Peter Piper picked?";

        Text myText = Text.Parse(sampleText);

        
        myText.PrintSortedByWordCount();
        
        myText.PrintSortedByLength();

        myText.FindWordsInInterrogative(4);

        myText.RemoveWordsStartingWithConsonant(3);
      
        foreach (var s in myText.Sentences)
            Console.WriteLine($"  {s}");

        Console.WriteLine("\n5. Замена слов длины 5 в первом предложении на 'TEST'");
        Console.WriteLine($"До замены:    {myText.Sentences[0]}");
        myText.ReplaceWordsInSentence(0, 5, "TEST");
        Console.WriteLine($"После замены: {myText.Sentences[0]}");

        Console.WriteLine("\n6. Удаление стоп-слов из текста");
        Console.WriteLine("Текст до удаления стоп-слов:");
        foreach (var s in myText.Sentences)
            Console.WriteLine($"  {s}");

        var stopWords = new HashSet<string>(
            new[] { "a", "of", "is", "the", "that" },
            StringComparer.OrdinalIgnoreCase);

        myText.RemoveStopWords(stopWords);
        Console.WriteLine("\nТекст после удаления стоп-слов:");
        foreach (var s in myText.Sentences)
            Console.WriteLine($"  {s}");

        myText.ExportToXml("text_output.xml");

        Text concordanceText = Text.Parse(sampleText);
        concordanceText.PrintConcordance();

        Console.WriteLine("\nГотово!");
    }
}