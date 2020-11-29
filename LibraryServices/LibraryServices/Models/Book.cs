using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryServices.Models
{
    public  class Book
    {
        public int BookID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        
        // Book Status is determined based on the status of book items.
        public string BookStatus { get; set; }
        public IList<Author> Authors { get; set; }
        
    }
}
