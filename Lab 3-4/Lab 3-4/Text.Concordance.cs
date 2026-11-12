using laba3_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace laba3_4
{
    public partial class Text
    {
        public Dictionary<string, (int TotalCount, SortedSet<int> Lines)> BuildConcordance()
        {
            var concordance =
                new Dictionary<string, (int TotalCount, SortedSet<int> Lines)>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (Sentence sentence in Sentences)
            {
                foreach (Word word in sentence.Words)
                {
                    string key = word.Value.ToLowerInvariant();

                    if (!concordance.TryGetValue(key, out var entry))
                        entry = (0, new SortedSet<int>());

                    entry.TotalCount++;
                    entry.Lines.Add(sentence.LineNumber);
                    concordance[key] = entry;
                }
            }

            return concordance;
        }

        public void PrintConcordance()
        {
            Console.WriteLine("\nКОНКОРДАНС");

            var concordance = BuildConcordance();

            foreach (var item in concordance.OrderBy(
                         pair => pair.Key, StringComparer.OrdinalIgnoreCase))
            {
                string lineNumbers = string.Join(" ", item.Value.Lines);
                Console.WriteLine(
                    $"{item.Key,-12} : {item.Value.TotalCount} раз(а) в строках: {lineNumbers}");
            }
        }
    }
}
