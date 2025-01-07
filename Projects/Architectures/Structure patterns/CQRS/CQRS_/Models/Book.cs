namespace CQRS_.Models
{
    internal class Book
    {
        public string Name { get; set; }

        public Book(string name)
        {
            Name = name;
        }
    }
}
