using Libary.DataAccess;
using Libary.Models;

namespace Libary.Logic
{
    public class LibaryLogic
    {
        private readonly IRepository repository;
        private List<Book> booksCache;

        public LibaryLogic(IRepository repository)
        {
            this.repository = repository;
            booksCache = repository.GetBooks();
        }

        public void AddBook(string id, string title, List<string> authors)
        {
            if (booksCache.Any(b => b.Id == id))
                throw new Exception("Book ID already exists.");
            if (string.IsNullOrEmpty(title) || title.Length < 4)
                throw new Exception("Title must be at least 4 characters long.");

            var newBook = new Book { Id = id, Title = title, Authors = authors, Genre = "Undefined" };
            booksCache.Add(newBook);

            LogAction("Create", title);
        }

        public void SetGenre(string id, string genre)
        {
            var book = booksCache.FirstOrDefault(b => b.Id == id);
            if (book == null)
                throw new Exception("Book not found.");

            book.Genre = genre;
            LogAction("SetGenre", book.Title);
        }

        public void UpdateAuthor(string oldAuthor, string newAuthor)
        {
            foreach (var book in booksCache)
            {
                if (book.Authors.Contains(oldAuthor))
                {
                    book.Authors = book.Authors.Select(a => a == oldAuthor ? newAuthor : a).ToList();
                }
            }

            LogAction("UpdateAuthor", $"All books for author {oldAuthor}");
        }

        public List<Book> ListBooks() => booksCache;

        private void LogAction(string action, string bookTitle)
        {
            var userId = CurrentUser.Instance.GetCurrentUserId();
            var userName = repository.GetUserNameById(userId);
            Console.WriteLine($"[{DateTime.Now}] {action}: '{bookTitle}' by User: {userName}");
        }
    }
}
