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
            "If Peter Piper picked a peck of pickled peppers, where is the peck that Peter Piper picked?";

        Text myText = Text.Parse(sampleText);

       
        myText.PrintSortedByWordCount();
        myText.PrintSortedByLength();
        myText.FindWordsInInterrogative(4);

        myText.RemoveWordsStartingWithConsonant(3);

        var stopWords = new HashSet<string>(
            new[] { "a", "of", "is", "the", "that" },
            StringComparer.OrdinalIgnoreCase);

        myText.RemoveStopWords(stopWords);
        myText.ExportToXml("text_output.xml");

       
        Text concordanceText = Text.Parse(sampleText);
        concordanceText.PrintConcordance();

        Console.WriteLine("\nГотово!");
    }
}