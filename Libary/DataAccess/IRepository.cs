﻿using Libary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.DataAccess
{
    internal interface IRepository
    {
        List<Book> GetBooks();
        void SaveBooks(List<Book> books);
        string GetUserNameById(int userId);
    }
}
