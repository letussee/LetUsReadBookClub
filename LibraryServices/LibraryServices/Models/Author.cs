using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryServices.Models
{
    public  class Author
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }

    }
}
