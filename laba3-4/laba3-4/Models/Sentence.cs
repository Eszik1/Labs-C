using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace laba3_4.Models
{
    [Serializable]
    public class Sentence
    {
        public List<Token> Tokens { get; set; } = new List<Token>();

        public int LineNumber { get; set; } = 1;

        [XmlIgnore]
        public IEnumerable<Word> Words => Tokens.OfType<Word>();

        public Sentence() { }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (int i = 0; i < Tokens.Count; i++)
            {
                Token current = Tokens[i];

                if (i > 0 && current is Word)
                {
                    Token previous = Tokens[i - 1];

                    if (previous is Word || (previous is Punctuation p && !IsOpeningPunctuation(p.Value)))
                    {
                        result.Append(' ');
                    }
                }

                result.Append(current.Value);
            }

            return result.ToString();
        }

        private static bool IsOpeningPunctuation(string value)
        {
            return value == "(" ||  value == "[" || value == "{"||
                   value == "«" || value == "“" || value == "‘"||
                   value == "\"" ||  value == "'";
        }
    }
}

