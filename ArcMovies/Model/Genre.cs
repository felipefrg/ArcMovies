using System.Collections.Generic;

namespace ArcMovies.Model
{
    public class Genres
    {
        public List<Genre> genres { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}