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
                throw new BookValidationException("Book ID must be a 12-character alphanumeric string.");
            else if (booksCache.Any(b => b.Id == id))
                throw new BookValidationException("Book ID already exists.");
            else if (string.IsNullOrEmpty(title) || title.Length < 4)
                throw new BookValidationException($"Title must be at least 4 characters long, '{title}' is not.");
            else if (!IsValidAuthorList(authors))
                throw new BookValidationException("Book must have at least one valid author.");

            var newBook = new Book { Id = id, Title = title, Authors = authors, Genre = genre };
            booksCache.Add(newBook);

            LogAction("Create", title);
        }

        public void SetGenre(string id, Genre genre)
        {
            var book = booksCache.FirstOrDefault(b => b.Id == id);
            if (book == null)
                throw new BookValidationException("Book not found.");

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
        private bool IsValidAuthorList(List<string> authors)
        {
            return authors != null && authors.All(author => !string.IsNullOrWhiteSpace(author)) && authors.Count > 0;
        }

        private void LogAction(string action, string bookTitle)
        {
            var userId = CurrentUser.Instance.GetCurrentUserId();
            var userName = repository.GetUserNameById(userId);
            Console.WriteLine($"[{DateTime.Now}] {action}: '{bookTitle}' by User: {userName}");
        }
    }
}
