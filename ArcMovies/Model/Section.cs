using System;
using System.Collections.Generic;

namespace ArcMovies.Model
{
    public class Section
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Movie> results { get; set; }
        public Dates dates { get; set; }
       
    }
}
