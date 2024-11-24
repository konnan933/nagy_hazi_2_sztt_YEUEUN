using Library.DataAccess;
using Library.Logic;

namespace Library
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var repository = new MemoryRepository();
            var libraryLogic = new LibaryLogic(repository);

            try
            {
                libraryLogic.AddBook("BOOK12345678", "Dune", new List<string> { "Frank Herbert" });
                libraryLogic.AddBook("BOOK87654321", "It", new List<string> { "Stephen King" });
                libraryLogic.AddBook("BOOK12349876", "1984", new List<string> { "George Orwell" });

                libraryLogic.AddBook("INVALIDID", "No", new List<string> { "Unknown" });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            libraryLogic.SetGenre("BOOK12345678", "Sci-fi");

            libraryLogic.UpdateAuthor("Stephen King", "S. King");

            var books = libraryLogic.ListBooks();
            Console.WriteLine("Books in library:");
            books.ForEach(Console.WriteLine);
        }
    }
}

