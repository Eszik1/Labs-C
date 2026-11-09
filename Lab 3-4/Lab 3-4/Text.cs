using laba3_4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace laba3_4
{
    [Serializable]
    public partial class Text
    {
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();

        public Text() { }

        
        public static Text Parse(string textContent)
        {
            if (textContent == null)
                throw new ArgumentNullException(nameof(textContent));

            var text = new Text();
            string[] lines = Regex.Split(textContent, @"\r\n|\n|\r");


            const string tokenPattern =
                @"[\p{L}\p{M}\p{N}]+(?:[-'’][\p{L}\p{M}\p{N}]+)*|[^\s]";

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                if (string.IsNullOrWhiteSpace(lines[lineIndex]))
                    continue;


                string[] sentenceParts =
                    Regex.Split(lines[lineIndex], @"(?<=[.!?])\s+");

                foreach (string part in sentenceParts)
                {
                    if (string.IsNullOrWhiteSpace(part))
                        continue;

                    var sentence = new Sentence { LineNumber = lineIndex + 1 };
                    MatchCollection matches = Regex.Matches(part, tokenPattern);

                    foreach (Match match in matches)
                    {
                        string value = match.Value;

                        if (Regex.IsMatch(value, @"^[\p{L}\p{M}\p{N}]+(?:[-'’][\p{L}\p{M}\p{N}]+)*$"))
                            sentence.Tokens.Add(new Word(value));
                        else
                            sentence.Tokens.Add(new Punctuation(value));
                    }

                    if (sentence.Tokens.Count > 0)
                        text.Sentences.Add(sentence);
                }
            }

            return text;
        }


        public void PrintSortedByWordCount()
        {
            Console.WriteLine("1. Предложения по возрастанию количества слов");

            foreach (Sentence sentence in Sentences.OrderBy(s => s.Words.Count()))
                Console.WriteLine($"[{sentence.Words.Count()} слов]: {sentence}");
        }


        public void PrintSortedByLength()
        {
            Console.WriteLine("\n2. Предложения по возрастанию длины");

            foreach (Sentence sentence in Sentences.OrderBy(s => s.ToString().Length))
                Console.WriteLine($"[{sentence.ToString().Length} симв.]: {sentence}");
        }

 
        public void FindWordsInInterrogative(int length)
        {
            Console.WriteLine($"\n3. Уникальные слова длины {length} в вопросительных предложениях");

            var words = Sentences
                .Where(s => s.Tokens.LastOrDefault() is Punctuation p && p.Value == "?")
                .SelectMany(s => s.Words)
                .Select(w => w.Value)
                .Where(w => w.Length == length)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(w => w, StringComparer.OrdinalIgnoreCase);

            foreach (string word in words)
                Console.WriteLine(word);
        }

 
        public void RemoveWordsStartingWithConsonant(int length)
        {
            Console.WriteLine($"\n4. Удаление слов длины {length}, начинающихся с согласной");

            const string vowels = "aeiouyаеёиоуыэюя";

            foreach (Sentence sentence in Sentences)
            {
                sentence.Tokens.RemoveAll(token =>
                {
                    if (token is not Word word || word.Value.Length != length)
                        return false;

                    char firstLetter = char.ToLowerInvariant(word.Value[0]);
                    return char.IsLetter(firstLetter) && !vowels.Contains(firstLetter);
                });
            }
        }

 
        public void ReplaceWordsInSentence(int sentenceIndex, int wordLength, string replacement)
        {
            if (replacement == null)
                throw new ArgumentNullException(nameof(replacement));

            if (sentenceIndex < 0 || sentenceIndex >= Sentences.Count)
            {
                Console.WriteLine("Предложения с таким индексом нет.");
                return;
            }

            Sentence sentence = Sentences[sentenceIndex];

            foreach (Token token in sentence.Tokens)
            {
                if (token is Word word && word.Value.Length == wordLength)
                    word.Value = replacement;
            }
        }


        public void RemoveStopWords(IEnumerable<string> stopWords)
        {
            if (stopWords == null)
                throw new ArgumentNullException(nameof(stopWords));

            var stopWordSet = new HashSet<string>(
                stopWords.Where(w => !string.IsNullOrWhiteSpace(w)),
                StringComparer.OrdinalIgnoreCase);

            foreach (Sentence sentence in Sentences)
            {
                sentence.Tokens.RemoveAll(token =>
                    token is Word word && stopWordSet.Contains(word.Value));
            }
        }

  
        public void ExportToXml(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не задан.", nameof(filePath));

            var serializer = new XmlSerializer(typeof(Text));

            using (var writer = new StreamWriter(filePath))
                serializer.Serialize(writer, this);

            Console.WriteLine($"\nТекст успешно экспортирован в XML: {filePath}");
        }
    }
}
