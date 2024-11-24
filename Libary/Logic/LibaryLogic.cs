using Library.DataAccess;
using Library.Models;
using System.Net;

namespace Library.Logic
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

        public void AddBook(string id, string title, List<string> authors, Genre genre)
        {
            if (!IsValidBookId(id))
                throw new ArgumentException("Book ID must be a 12-character alphanumeric string.");
            else if (booksCache.Any(b => b.Id == id))
                throw new Exception("Book ID already exists.");
            else if (string.IsNullOrEmpty(title) || title.Length < 4)
                throw new Exception($"Title must be at least 4 characters long, '{title}' is not.");

            var newBook = new Book { Id = id, Title = title, Authors = authors, Genre = genre };
            booksCache.Add(newBook);

            LogAction("Create", title);
        }

        public void SetGenre(string id, Genre genre)
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

        private bool IsValidBookId(string bookId)
        {
            return !string.IsNullOrEmpty(bookId) &&
                   bookId.Length == 12 &&
                   bookId.All(char.IsLetterOrDigit);
        }

        private void LogAction(string action, string bookTitle)
        {
            var userId = CurrentUser.Instance.GetCurrentUserId();
            var userName = repository.GetUserNameById(userId);
            Console.WriteLine($"[{DateTime.Now}] {action}: '{bookTitle}' by User: {userName}");
        }
    }
}
