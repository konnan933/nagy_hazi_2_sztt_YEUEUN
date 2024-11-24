using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class MemoryRepository : IRepository
    {
        private List<Book> books = new List<Book>();
        private readonly Dictionary<int, string> users = new()
        {
            { 1, "Admin" },
            { 2, "Jancsika" },
            { 17, "Attila" }
        };

        public List<Book> GetBooks() => books;

        public void SaveBooks(List<Book> books) => this.books = books;

        public string GetUserNameById(int userId) => users.TryGetValue(userId, out string? value) ? value : "Unknown";
    }
}
