using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LibraryServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibraryServices.Controllers
{
    
    [ApiController]
    //[Route("api/[Controller]")]
    public class BookController :ControllerBase
    {
        private readonly ILogger<BookController> log;
        DataAccessLayer.DataAccessLayer dt = new DataAccessLayer.DataAccessLayer();
        public BookController(ILogger<BookController> logger)
        {
            log = logger;
        }
        
       // [Authorize]
        [Route("Book")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]  
        public List<Book> GetAllBooks()
        {
            List<Book> lstBooks = null;
            try
            {
                lstBooks = dt.GetAllBookDetails();
            }
            catch (Exception ex)
            {
                log.LogError("Error while fetching all book details", ex.Message, ex.StackTrace.ToString());
                lstBooks = JsonConvert.DeserializeObject<List<Book>>(System.IO.File.ReadAllText(AppSettings.SampleFile));
            }
            return lstBooks;
        }
        [HttpGet]
        [Route("/")]
        public IEnumerable<string> WelcomeBook()
        {
            return new string[] { "Hi There! You logged into Book Controller" };
            
        }
    }
}
