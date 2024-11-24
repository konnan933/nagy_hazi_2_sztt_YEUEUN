using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public List<string> Authors { get; set; }


        public override string ToString()
        {
            return $"{Id}: {Title} ({Genre}), Authors: {string.Join(", ", Authors)}";
        }
    }
}
