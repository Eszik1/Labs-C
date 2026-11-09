using System;
using System.Xml.Serialization;

namespace laba3_4.Models
{
    [Serializable]
    [XmlInclude(typeof(Word))]
    [XmlInclude(typeof(Punctuation))]
    public abstract class Token
    {
        public string Value { get; set; }
        protected Token() { }
        protected Token(string value) { Value = value; }
        public override string ToString() => Value;
    }
    [Serializable]
    public class Word : Token
    {
        public Word() { }
        public Word(string value) : base(value) { }
    }

    [Serializable]
    public class Punctuation : Token
    {
        public Punctuation() { }
        public Punctuation(string value) : base(value) { }
    }
}
