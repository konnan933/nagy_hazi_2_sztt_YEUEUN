using Libary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.DataAccess
{
    public class MemoryRepository : IRepository
    {
        private List<Book> books = new List<Book>();
        private readonly Dictionary<int, string> users = new()
        {
            { 1, "Admin" },
            { 2, "Jancsika" }
        };

        public List<Book> GetBooks() => books;

        public void SaveBooks(List<Book> books) => this.books = books;

        public string GetUserNameById(int userId) => users.TryGetValue(userId, out string? value) ? value : "Unknown";
    }
}
