using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using LibraryServices.Models;

namespace LibraryServices.DataAccessLayer
{
    public class DataAccessLayer
    {
       public string ConnnectionString= "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppSettings.DataBasePath;
       

        public DataTable ExecuteSelectQueryFetchAll(string query)
        {
            DataTable dtResult = null;
            if (!string.IsNullOrEmpty(query))
            {
                dtResult = new DataTable();

                
                using (OleDbConnection con = new OleDbConnection(ConnnectionString))
                {
                    con.Open();
                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        using (OleDbCommand cmd = con.CreateCommand())
                        {
                            da.SelectCommand = cmd;
                            cmd.CommandText = query;
                            cmd.CommandType = CommandType.Text;
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            dtResult = ds.Tables[0];
                        }
                    }
                }
            }
            return dtResult;
        }

        public List<Book> getAllBooks()
        {
            DataTable dtBooks = ExecuteSelectQueryFetchAll("Select * from Book");
            List<Book> lstBooks= new List<Book>();
            if(dtBooks!=null)
            {
                Book book = null;
                foreach (DataRow drBooks in dtBooks.Rows)
                {
                    book = new Book
                    {
                        BookID = Convert.ToInt32(drBooks["ID"]?.ToString()),
                        ISBN = drBooks["ISBN"]?.ToString(),
                        Title = drBooks["Title"]?.ToString()
                    };
                    lstBooks.Add(book);
                }
            }

            return lstBooks;
        }
        public List<Author> getAllAuthors()
        {
            DataTable dtAuthors = ExecuteSelectQueryFetchAll("Select * from Author");
         
            List<Author> lstAuthors = new List<Author>();
            if (dtAuthors != null)
            {
                Author Author = null;
                foreach (DataRow drAuthors in dtAuthors.Rows)
                {
                    Author = new Author
                    {
                        AuthorID= Convert.ToInt32(drAuthors["ID"]?.ToString()),
                        AuthorName = drAuthors["AuthorName"]?.ToString(),
                        Description = drAuthors["AuthorDescription"]?.ToString()
                    };
                    lstAuthors.Add(Author);
                }
            }

            return lstAuthors;
        }
        public List<BookItem> getAllBookItems()
        {
            DataTable dtBookItems = ExecuteSelectQueryFetchAll("Select * from BookItem");
            List<BookItem> lstBookItems = new List<BookItem>();
            if (dtBookItems != null)
            {
                BookItem BookItem = null;
                foreach (DataRow drBookItems in dtBookItems.Rows)
                {
                    BookItem = new BookItem
                    {
                        BookItemID = Convert.ToInt32(drBookItems["ID"]?.ToString()),
                        Barcode = drBookItems["Barcode"]?.ToString(),
                        BookItemStatus = drBookItems["BookItemStatus"]?.ToString(),
                        BookID= Convert.ToInt32(drBookItems["BookId"]?.ToString())
                    };
                    lstBookItems.Add(BookItem);
                }
            }
            return lstBookItems;
        }
        public List<BookAuthor> getAllBookAuthor()
        {
            DataTable dtBookAuthor = ExecuteSelectQueryFetchAll("Select * from BookAuthor");
            List<BookAuthor> lstBookAuthor = new List<BookAuthor>();
            if (dtBookAuthor != null)
            {
                BookAuthor BookAuthor = null;
                foreach (DataRow drBookAuthor in dtBookAuthor.Rows)
                {
                    BookAuthor = new BookAuthor
                    {
                        BookAuthorID = Convert.ToInt32(drBookAuthor["ID"]?.ToString()),
                        BookId = Convert.ToInt32(drBookAuthor["BookId"]?.ToString()),
                        AuthorId =Convert.ToInt32( drBookAuthor["AuthorId"]?.ToString())
                    };
                    lstBookAuthor.Add(BookAuthor);
                }
            }
            return lstBookAuthor;
        }

        public List<Book> GetAllBookDetails()
        {
            List<Book> lstBooks = null;
            try
            {
                lstBooks = getAllBooks();
                if (lstBooks?.Count > 0)
                {
                    List<BookItem> lstBookItems = getAllBookItems();
                    if (lstBookItems?.Count > 0)
                    {
                        foreach (var book in lstBooks)
                        {
                            var bookItem = lstBookItems.Where(a => a.BookID == book.BookID).Select(a => a).ToList();
                            if (bookItem?.Count > 0)
                            {
                                // book.BookItems = bookItem;
                                var status = bookItem.Where(a => a.BookItemStatus == "A").Select(a => a).ToList();
                                book.BookStatus = "Rented";
                                if (status?.Count > 0)
                                {
                                    book.BookStatus = "Available";
                                }
                            }

                        }
                    }


                    List<BookAuthor> lstBookAuthor = new List<BookAuthor>();
                    lstBookAuthor = getAllBookAuthor();

                    List<Author> lstAuthor = getAllAuthors();
                    if (lstAuthor?.Count > 0)
                    {
                        foreach (var Book in lstBooks)
                        {
                            Book.Authors = new List<Author>();
                            Book.Authors = lstAuthor.Where(a => lstBookAuthor.Any(e => e.BookId == Book.BookID && e.AuthorId == a.AuthorID)).ToList();
                        }

                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return lstBooks;
        }

        }

    }
