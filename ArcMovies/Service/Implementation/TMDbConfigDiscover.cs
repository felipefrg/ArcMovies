using System;
using System.Collections.Generic;

using System.Text;
using ArcMovies.Service.Abstraction;

namespace ArcMovies.Service.Implementation
{
    public class TMDbConfigDiscover : TMDbConfig
    {

        public TMDbConfigDiscover() : base(Helper.AppConst.TMDbRootURL+"3/discover")
        {

        }

        public TMDbConfigDiscover ActionMovie()
        {
            this._action = "/movie?";
            return this;
        }
        public TMDbConfigDiscover WithCredits()
        {
            this._lstAppend.Add("credits");
            return this;
        }

        public TMDbConfigDiscover WithGenres(int id)
        {
            this._lstGenre.Add(id);
            return this;
        }

        public TMDbConfigDiscover WithVideos()
        {
            this._lstAppend.Add("videos");
            return this;
        }
    }
}
