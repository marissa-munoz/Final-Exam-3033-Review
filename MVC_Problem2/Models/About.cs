using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Problem2.Models
{
    public class About
    {
        public string Head_Coach { get; set; }
        public string Bowl_Record { get; set; }
        public string National_Titles { get; set; }


        public About()
        {
            Head_Coach = "Lincoln Riley";
            Bowl_Record = "29-22-1 (.567)";
            National_Titles = "7 (1950, 1955, 1956, 1974, 1975, 1985, 2000)";
        }
    }
}