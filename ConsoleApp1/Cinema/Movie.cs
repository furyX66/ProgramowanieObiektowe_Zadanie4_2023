using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Cinema
{
    public class Movie
    {
        #region Public Members
        public string? Title { get; set; }
        public int Duration { get; set; }
        public int AgeRestriction { get; set; }
        #endregion
        #region Public Methods
        public void Show() //Method to show information about movies
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Duration: {Duration} minutes");
            Console.WriteLine($"Age Restriction: {AgeRestriction}+");
        }
        #endregion
    }
}
