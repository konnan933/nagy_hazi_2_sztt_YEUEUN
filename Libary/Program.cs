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
                libraryLogic.AddBook("BOOK12345678", "Dune", new List<string> { "Frank Herbert" }, Genre.Drama);
                libraryLogic.AddBook("BOOK91235412", "Dune II", new List<string> { "Frank Herbert" }, Genre.SciFi);
                libraryLogic.AddBook("BOOK12349876", "1984", new List<string> { "George Orwell" }, Genre.SciFi);
                //libraryLogic.AddBook("BOOK87654321", "It", new List<string> { "Stephen King" }, Genre.Horror);

                libraryLogic.AddBook("INVALIDID", "No", new List<string> { "Unknown" }, Genre.Classic);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            libraryLogic.SetGenre("BOOK12345678", Genre.SciFi);

            libraryLogic.UpdateAuthor("Stephen King", "S. King");

            var books = libraryLogic.ListBooks();
            Console.WriteLine("Books in library:");
            books.ForEach(Console.WriteLine);
        }
    }
}

