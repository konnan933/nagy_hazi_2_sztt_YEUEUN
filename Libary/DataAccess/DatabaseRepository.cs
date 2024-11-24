using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class DatabaseRepository : IRepository
    {
        public List<Book> GetBooks() => new List<Book>();
        public void SaveBooks(List<Book> books) { /* kód */ }
        public string GetUserNameById(int userId) => "FixedName"; 
    }
}
