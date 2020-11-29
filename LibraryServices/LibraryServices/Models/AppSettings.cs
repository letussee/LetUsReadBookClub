using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryServices.Models
{
    public class AppSettings
    {
        public static string DataBasePath { get; set; }
        public static string LogFile { get; set; }
        public static string AllowedOrigins { get; set; }
        public static string SampleFile { get; set; }
    }
}
