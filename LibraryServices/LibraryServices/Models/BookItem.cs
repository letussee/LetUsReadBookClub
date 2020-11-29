using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryServices.Models
{
    public class BookItem :Book    {
        
        public int BookItemID { get; set; }
        public string Barcode {get;set;}
       public string BookItemStatus { get; set; }
    }
}
