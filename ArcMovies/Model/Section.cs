using System;
using System.Collections.Generic;

namespace ArcMovies.Model
{
    public class Section
    {
        public int Page { get; set; }
        public int Total_Results { get; set; }
        public int Total_Pages { get; set; }
        public List<Movie> Results { get; set; }
    }
}
