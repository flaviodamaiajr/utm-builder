namespace UtmBuilder.Core.ValueObjects
{
    public class Campaign : ValueObject
    {
        public Campaign(int id, string source, string medium, string name, string term, string content)
        {
            Id = id;
            Source = source;
            Medium = medium;
            Name = name;
            Term = term;
            Content = content;
        }

        public int Id { get; }
        public string Source { get; }
        public string Medium { get; }
        public string Name { get; }
        public string Term { get; }
        public string Content { get; }
    }
}