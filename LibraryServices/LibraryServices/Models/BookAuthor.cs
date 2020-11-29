using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryServices.Models
{
    public class BookAuthor
    {
        public int BookAuthorID { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }
        public string Description { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}