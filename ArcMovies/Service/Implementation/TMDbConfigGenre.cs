using System;
using System.Collections.Generic;

using System.Text;
using ArcMovies.Service.Abstraction;

namespace ArcMovies.Service.Implementation
{
    public class TMDbConfigGenre : TMDbConfig
    {
        public TMDbConfigGenre() : base(Helper.AppConst.TMDbRootURL+"3/genre/movie")
        {
            this._action = "/list?";
        }
    }
}
